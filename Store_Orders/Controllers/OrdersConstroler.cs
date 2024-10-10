using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersConstroler : ControllerBase
    {
        private readonly ShopData _context;
        public OrdersConstroler(ShopData context)
        {
            _context = context;
        }

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
