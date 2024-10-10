using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary.ModelShop;

namespace Store_Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController: ControllerBase
    {
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
