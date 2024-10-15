using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace LibraryManagement
{ 
    public class JwtManager
    {
        // Method to generate JWT token
        public static string GenerateToken(string username, int expireMinutes = 20)
        {
            var key = Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["JwtSecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = System.Configuration.ConfigurationManager.AppSettings["JwtIssuer"],
                Audience = System.Configuration.ConfigurationManager.AppSettings["JwtAudience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Method to validate JWT token
        public static ClaimsPrincipal ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["JwtSecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = System.Configuration.ConfigurationManager.AppSettings["JwtIssuer"],
                ValidAudience = System.Configuration.ConfigurationManager.AppSettings["JwtAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null; // Invalid token
            }
        }
    }
}
