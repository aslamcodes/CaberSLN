﻿using Caber.Models;
using Caber.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Caber.Services
{
    public class TokenService : ITokenService
    {
        public readonly string _secretKey;
        public readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("TokenKey").GetSection("key").Value.ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        }
        public string GenerateUserToken(User user)
        {

            var claims = new List<Claim>(){
                new("uid",user.Id.ToString()),
                new(ClaimTypes.Role, user.UserType.ToString())
            };
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(myToken);

            return token;
        }
    }
}
