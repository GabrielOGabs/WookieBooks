using Api.WookieBooks.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.WookieBooks.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.WookieBooks.Helpers
{
    public class JwtTokenGeneratorHelper
    {
        private readonly AppSettings _appSettings;

        public JwtTokenGeneratorHelper(AppSettings appSettings)
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
    }
}
