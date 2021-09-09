using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BurgerBackend.Identity.Service.Config;
using BurgerBackend.Identity.Service.Services.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BurgerBackend.Identity.Service.Services
{
    internal sealed class TokenService : ITokenService
    {
        private readonly TimeSpan ExpirationDuration = TimeSpan.FromMinutes(30);

        private readonly JwtConfig _jwtConfig;

        public TokenService(IOptions<JwtConfig> jwtConfigOptions)
        {
            _jwtConfig = jwtConfigOptions.Value;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                { 
                    new Claim(nameof(ApplicationUser.Email), user.Email),
                    new Claim(nameof(ApplicationUser.Id), user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.Add(ExpirationDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        public Guid? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == nameof(ApplicationUser.Id)).Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
