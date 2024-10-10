using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;

namespace Store_Category.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryControler : ControllerBase
    {
        private readonly ShopData _context;
        public CategoryControler(ShopData context)
        {
            _context = context;
        }

    }
}
