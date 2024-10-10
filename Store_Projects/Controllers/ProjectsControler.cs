using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Projects.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsControler: ControllerBase
    {
        private readonly ShopData _context;
        public ProjectsControler(ShopData context)
        {
            _context = context;
        }

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
