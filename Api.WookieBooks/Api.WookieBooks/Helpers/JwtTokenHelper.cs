using Api.WookieBooks.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.WookieBooks.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace Api.WookieBooks.Helpers
{
    public class JwtTokenHelper
    {
        private readonly AppSettings _appSettings;

        public JwtTokenHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void GenerateToken(AuthorizedUserDto authorizedUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var ssKeySecret = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var ssKey = new SymmetricSecurityKey(ssKeySecret);
            string algorithm = SecurityAlgorithms.HmacSha256Signature;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authorizedUser.FullName.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, authorizedUser.Id.ToString()),
                    new Claim(ClaimTypes.Actor, authorizedUser.Login)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(ssKey, algorithm)
            };

            var authorizationToken = tokenHandler.CreateToken(tokenDescriptor);
            authorizedUser.AuthToken = tokenHandler.WriteToken(authorizationToken);
        }

        public int GetUserIdFromClaims(ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity
                .Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .Single();

            return Convert.ToInt32(userId);
        }
    }
}
