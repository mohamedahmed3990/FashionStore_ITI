using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FashionStore.PL.Helpers
{
    public static class Utilities
    {
        public static string GenerateToken(List<Claim> claims , IConfiguration configure , bool rememberMe = false)
        {
            var secretkey = configure.GetValue<string>("JwtSecretKey");
            var keyinBytes = Encoding.UTF8.GetBytes(secretkey);
            var key = new SymmetricSecurityKey(keyinBytes);

            var expires = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(1);

            var token = new JwtSecurityToken(
                    expires: expires,
                    claims: claims,
                    signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );

            var TokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return TokenString;
        }

        public static string GenerateRefreshToken()
        {
            var randomBytes = new Byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            };
            
        }

    }
}
