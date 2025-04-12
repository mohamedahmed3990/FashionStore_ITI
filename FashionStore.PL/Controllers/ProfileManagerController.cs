using FashionStore.BLL.DTOs;
using FashionStore.DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileManagerController : ControllerBase
    {
        private readonly IValidator<ChangePasswordDTO> _changePasswprdValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<PersonalInformationDTO> _personalInformationValidator;
        private readonly IValidator<UserAddressDto> _addressDetailsValidator;

        public ProfileManagerController(
            IValidator<ChangePasswordDTO> changePasswprdValidator, UserManager<AppUser> userManager,
            IValidator<PersonalInformationDTO> personalInformationValidator,
            IValidator<UserAddressDto> addressDetailsValidaors
            )
        {
            _changePasswprdValidator = changePasswprdValidator;
            _userManager = userManager;
            _personalInformationValidator = personalInformationValidator;
            _addressDetailsValidator = addressDetailsValidaors;
        }

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

        [HttpGet("Personal-information")]
        [Authorize]
        public async Task<ActionResult> GetPersonalInformation()
        {
            try
            {
                
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return Unauthorized("User not found.");

                var userDto = new PersonalInformationDTO()
                {
                    Email = user.Email,
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName ?? ""
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve user data.");
            }
        }

        [HttpPut("Update-Personal-information")]
        [Authorize]
        public async Task<ActionResult> UpdatePersonalInformation(PersonalInformationDTO model)
        {
            try
            {
                var ValidationResult = await _personalInformationValidator.ValidateAsync(model);
                if(!ValidationResult.IsValid)
                    return BadRequest(ValidationResult.Errors);


                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized("User not found.");

                user.FirstName = model.FirstName ?? user.FirstName;
                user.LastName = model.LastName ?? user.LastName;
                user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
                user.UserName = model.UserName ?? user.UserName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("Data Has been Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Update user data.");
            }
        }

        [HttpGet("Address-Details")]
        [Authorize]
        public async Task<ActionResult> GetAddressDetails()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return Unauthorized("User not found.");

                var userAddressDto = new UserAddressDto()
                {
                    Country = user.Country,
                    City = user.City,
                    AddressDetails = user.AddressDetails,
                };

                return Ok(userAddressDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve user address.");
            }
        }

        [HttpPut("Update-Address-Details")]
        [Authorize]
        public async Task<ActionResult> UpdateAddressDetails(UserAddressDto model)
        {
            try
            {
                var ValidationResult = await _addressDetailsValidator.ValidateAsync(model);
                if (!ValidationResult.IsValid)
                    return BadRequest(ValidationResult.Errors);


                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized("User not found.");

                user.Country = model.Country ?? user.Country;
                user.City = model.City ?? user.City;
                user.AddressDetails = model.AddressDetails ?? user.AddressDetails;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("Address Has been Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Update user address.");
            }
        }

        [HttpPut("Remove-Address-Details")]
        [Authorize]
        public async Task<ActionResult> RemoveAddressDetails()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized("User not found.");

                user.Country = null;
                user.City = null;
                user.AddressDetails = null;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("Address Has been Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Delete user address.");
            }
        }

    }
}
