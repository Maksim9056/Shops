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
            try
            {


                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    throw new Exception("Неверные учетные данные");
                }

                user.CreatedDate = DateTime.UtcNow;
                //await _dbContext.SaveChangesAsync();

                var token = GenerateJwtToken(user);
                return token;
            }
            catch (Exception ex)
            {
               return ex.Message;
            }
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            try
            {
             
                // Хэшируем пароль пользователя
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var productImage = await _dbContext.Image.Include(u => u.ImageCopies).FirstOrDefaultAsync(U => U.Id == user.Id_User_Image.Id);

                // Проверяем, существует ли статус в базе данных
                var existingStatus = await _dbContext.Status.FindAsync(user.Status.Id);
                if (existingStatus != null)
                {
                    _dbContext.Entry(productImage).State = EntityState.Unchanged;
                    user.Id_User_Image = productImage;
                    // Если статус существует, присоединяем его к контексту
                    _dbContext.Entry(existingStatus).State = EntityState.Unchanged;
                    user.Status = existingStatus; // Указываем, что это существующий статус
                }

                await   _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public async Task<User> User_Product(long Id, string email)
        {
            User user = new User();
            try
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id && u.Email == email);


                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
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
