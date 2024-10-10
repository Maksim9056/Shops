using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserConroller: ControllerBase
    {
        private readonly ShopData _context;
        public UserConroller(ShopData context)
        {
            _context = context;
        }


        [HttpGet("log")]
        public async Task UserLog()
        {


        }

        [HttpGet("check-similar")]
        public async Task UserCheckSimular()
        {


        }


        [HttpPost]
        public ActionResult UserCreate([FromBody] User User)
        {
           return   Ok();
        }
    }
}
