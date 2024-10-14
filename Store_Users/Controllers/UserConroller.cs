using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store_Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserConroller: ControllerBase
    {
        private readonly string _secret;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration  _iconfiguration;
        private readonly IUserService _IUserService;

        private readonly ShopData _context;
        public UserConroller(ShopData context, IMemoryCache cache, IConfiguration config, IUserService iUserService)
        {
            _cache = cache;
            _iconfiguration = config;
            _context = context;
            _IUserService = iUserService;
        }


        [HttpGet("log")]
        public async Task UserLog()
        {
           await    _IUserService.AuthenticateUserAsync("","");

        }

        [HttpGet("check-similar")]
        public async Task UserCheckSimular()
        {


        }
     

        [HttpPost]
        public async Task<ActionResult> UserCreate([FromBody] User User)
        {
            try
            {
            
                var users = await _IUserService.RegisterUserAsync(User);

                
                ////_context.Add(User);
                return Ok(User);

            }
            catch (Exception ex) 
            {
                NotFound(ex.Message);
            }
            return Ok(User);
        }

       
    }
}
