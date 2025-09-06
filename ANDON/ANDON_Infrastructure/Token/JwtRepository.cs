using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;
using ANDON_Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace ANDON_Infrastructure.Token
{
    public class JwtRepository : IJwtRepository
    {
        private readonly IConfiguration _configuration;
        public JwtRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(string username, string password, string groupId)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            string? secretKey = jwtSettings["Secret"];
            string? issuer = jwtSettings["Issuer"];
            string? audience = jwtSettings["Audience"];
            string? expiresIn = jwtSettings["ExpiresInMinutes"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("UserName",username),
                new Claim("Password", password),
                new Claim("GroupId",groupId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenIss = new JwtSecurityToken(
                issuer:issuer,
                audience:audience,
                claims:claims,
                 expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(expiresIn)),
                signingCredentials: credentials

            );
            return new JwtSecurityTokenHandler().WriteToken(tokenIss);
        }
    }
}
