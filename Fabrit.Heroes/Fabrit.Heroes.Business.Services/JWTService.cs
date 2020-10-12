using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Fabrit.Heroes.Data.Entities.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Fabrit.Heroes.Business.Services
{
    public class JWTService : IJWTService
    {
        private readonly AppSettings _appSettings;
        public JWTService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public int DecodeAuthenticationJWT(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return Int32.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            }
            catch
            {
                throw new UnauthorizedAccessException("Invalid token");
            }
        }

        public string DecodeRegisterToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return jwtToken.Claims.First(x => x.Type == "email").Value;
            }
            catch
            {
                throw new UnauthorizedAccessException("Invalid token");
            }
        }

        public string GenerateAuthenticationJWT(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim(ClaimTypes.Role, user.Role.Name) });
            var expires = DateTime.UtcNow.AddHours(6);
            return GenerateJWTToken(claimsIdentity, expires);
        }

        public string GenerateRegisterJWT(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[] { new Claim("email", user.Email) });
            var expires = DateTime.UtcNow.AddHours(48);
            return GenerateJWTToken(claimsIdentity, expires);
        }

        private string GenerateJWTToken(ClaimsIdentity claimsIdentity, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
