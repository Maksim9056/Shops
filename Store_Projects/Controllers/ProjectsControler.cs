using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary.ModelShop;

namespace Store_Projects.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsControler: ControllerBase
    {
        [HttpGet]
        public async Task Projects()
        {


        }

        [HttpPost]
        public async Task ProjectsCreate([FromBody] Project Project)
        {

        }

    }
}
