using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store_Users.Service
{
    public class UserService: IUserService
    {
        private readonly ShopData _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _iconfiguration;

        private readonly string _secret;

        public UserService(ShopData dbContext, IMemoryCache cache, IConfiguration config)
        {
            _dbContext = dbContext;
            _cache = cache;
            _iconfiguration = config;

            _secret = config["AppSettings:Secret"];
        }




        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Неверные учетные данные");
            }

            user.CreatedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return token;
        }

     

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
