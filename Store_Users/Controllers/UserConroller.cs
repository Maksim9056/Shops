using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary.ModelShop;

namespace Store_Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserConroller: ControllerBase
    {
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
