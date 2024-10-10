using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary.ModelShop;

namespace Store_Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersConstroler : ControllerBase
    {


        [HttpGet]
        public  async Task OrdersGet(User user)
        {

           
        }

        [HttpPost]
        public async Task OrderCreate([FromBody] Order order)
        {

        }
    }
}
