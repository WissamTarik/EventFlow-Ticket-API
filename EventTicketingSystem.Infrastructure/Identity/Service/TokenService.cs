using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Identity.Service
{
    public class TokenService(IOptions<JWTOptions> options) : ITokenService
    {
        private readonly JWTOptions optionsValue = options.Value;
        private readonly List<Claim> userClaims = [];
        public string GenerateToken(string userId, string username, string email, IReadOnlyList<string> roles)
        {

            userClaims.Add(new(ClaimTypes.NameIdentifier, userId));
            userClaims.Add(new(ClaimTypes.Name, username));
            userClaims.Add(new(ClaimTypes.Email, email));
            if(roles.Count>0)
                foreach (var role in roles)
                {
                    userClaims.Add(new(ClaimTypes.Role, role));
                }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionsValue.SecretKey));
            var token = new JwtSecurityToken(
                issuer: optionsValue.Issuer,
                audience: optionsValue.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(optionsValue.DurationInDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
