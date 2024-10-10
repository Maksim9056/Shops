using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController: ControllerBase
    {
        private readonly ShopData _context;
        public TaskController(ShopData context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task Tasks_Shop_Get()
        {


        }


        [HttpPost]
        public async Task Tasks_ShopCreate([FromBody] Tasks_Shop Tasks)
        {

        }
    }
}
