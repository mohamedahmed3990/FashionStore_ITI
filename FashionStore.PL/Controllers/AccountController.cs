using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;
using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.AuthServices;
using FashionStore.BLL.Services.TokenServices;
using FashionStore.BLL.Validators;
using FashionStore.DAL.Entities;
using FashionStore.PL.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<RegisterDto> _validator;
        private readonly IValidator<LoginDto> _loginValidatio;
        private readonly IConfiguration _config;
        private readonly TokenServices _tokenServices;
        private readonly EmailService _emailService;
        private readonly IValidator<ResetPasswordDTO> _resetPasswprdValidator;
        private readonly IValidator<ChangePasswordDTO> _changePasswprdValidator;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, IValidator<RegisterDto> validator,
            IValidator<LoginDto> LoginValidatio, IConfiguration config, TokenServices tokenServices,
            EmailService emailService, IValidator<ResetPasswordDTO> resetPasswprdValidator,
            IValidator<ChangePasswordDTO> changePasswprdValidator,
            SignInManager<AppUser> signInManager
            )
        {
            _userManager = userManager;
            _validator = validator;
            _loginValidatio = LoginValidatio;
            _config = config;
            _tokenServices = tokenServices;
            _emailService = emailService;
            _resetPasswprdValidator = resetPasswprdValidator;
            _changePasswprdValidator = changePasswprdValidator;
            _signInManager = signInManager;
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            var ValidationResult = await _validator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return BadRequest("Username already exists.");

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return BadRequest("Email already exists.");

            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors
                .Select(e => e.Description)
                .ToList();

                return BadRequest(errors);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            await _userManager.AddClaimsAsync(user, claims);

            return Ok("User registered and claims added.");
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            var ValidationResult = await _loginValidatio.ValidateAsync(model);
            if (!ValidationResult.IsValid)
                return BadRequest(ValidationResult.Errors);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized();

            var IsPasswordRight = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!IsPasswordRight)
                return Unauthorized();

            var claims = await _userManager.GetClaimsAsync(user);
            var token = Utilities.GenerateToken(claims.ToList(), _config, model.RememberMe);
            var refreshToken = Utilities.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = token,
                refreshToken = refreshToken
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken(RefreshTokenDto model)
        {
            if (model is null)
                return BadRequest("Invalid client request");

            var principal = _tokenServices.GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal == null)
                return BadRequest("Invalid access token or refresh token");

            var username = principal?.Identity?.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpireDate <= DateTime.UtcNow)
            {
                return BadRequest("Invalid refresh token");
            }

            var newAccessToken = Utilities.GenerateToken(principal.Claims.ToList(), _config);
            var newRefreshToken = Utilities.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid User");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var EncodedToken = WebUtility.UrlEncode(resetToken);

            var clientUrl = _config["ClientUrl"];
            var resetLink = $"{clientUrl}/reset-password?email={model.Email}&token={EncodedToken}";
            var subject = "Password Reset Request";
            var emailBody = $"Click the link below to reset your password:<br/><a href='{resetLink}'>Reset Password</a>";

            await _emailService.SendEmailAsync(model.Email, subject, emailBody);

            return Ok("Password reset link has been sent.");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var validationResult = _resetPasswprdValidator.Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("email doesnt exist");


            var decodedToken = WebUtility.UrlDecode(model.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password has been reset successfully");

        }

        [HttpGet("external-login")]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")

        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId: null);
            return Challenge(properties, provider);
        }


        #region Working Endpoint with cookie not authentication
        // This is the working one with cookie authentication not token
        //[HttpGet("external-login-callback")]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        //{
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //        return Redirect("/login-failed");

        //    //Sign in or register the user
        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

        //    if (result.Succeeded)
        //    {
        //        return Redirect(returnUrl);
        //    }

        //    // Register the user if they don't exist
        //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    var user = new AppUser { UserName = email, Email = email };

        //    var identityResult = await _userManager.CreateAsync(user);
        //    if (!identityResult.Succeeded)
        //        return BadRequest(identityResult.Errors);

        //    await _userManager.AddLoginAsync(user, info);
        //    await _signInManager.SignInAsync(user, isPersistent: false);

        //    //Creating the claims

        //    //var claims = new List<Claim>
        //    //{
        //    //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    //    new Claim(ClaimTypes.Name, user.UserName),
        //    //    new Claim(ClaimTypes.Email, user.Email),
        //    //};

        //    //var token = Utilities.GenerateToken(claims.ToList(), _config);
        //    //var refreshToken = Utilities.GenerateRefreshToken();

        //    //user.RefreshToken = refreshToken;
        //    //user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);

        //    //await _userManager.UpdateAsync(user);

        //    return Redirect(returnUrl);

        //    //return Ok(new {token , refreshToken});

        //} 
        #endregion

        #region Working Endpoint with JWT token not cookie
        // Working Endpoint with JWT token not cookie
        [HttpGet("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Redirect("/login-failed");

            // Sign in or register the user
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                // User is already logged in, generate JWT token
                var googleUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var googleClaims = await _userManager.GetClaimsAsync(googleUser);

                // Add additional claims if needed (e.g., user roles, custom claims)
                googleClaims.Add(new Claim(ClaimTypes.Name, googleUser.UserName));
                googleClaims.Add(new Claim(ClaimTypes.Email, googleUser.Email));

                // Create the JWT token
                var accessToken = Utilities.GenerateToken(googleClaims.ToList(), _config);
                var refreshToken = Utilities.GenerateRefreshToken();

                googleUser.RefreshToken = refreshToken;
                googleUser.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);

                await _userManager.UpdateAsync(googleUser);

                return Ok(new { accessToken, refreshToken });
            }

            // Register the user if they don't exist
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = new AppUser { UserName = email, Email = email };

            var identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
                return BadRequest(identityResult.Errors);

            await _userManager.AddLoginAsync(user, info);

            // Create JWT token for new user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var accessedToken = Utilities.GenerateToken(claims.ToList(), _config);
            var refreshedToken = Utilities.GenerateRefreshToken(); ;

            user.RefreshToken = refreshedToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return Ok(new { accessedToken, refreshedToken });
        } 
        #endregion

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var validationResult = _changePasswprdValidator.Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized("User not found.");

            // Attempt to change password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password changed successfully.");
        }


    }
}
