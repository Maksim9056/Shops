using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;

namespace Store_Product_category.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Product_CategoryControllers: ControllerBase
    {
        private readonly ShopData _context;
        public Product_CategoryControllers(ShopData context)
        {
            _context = context;
        }
    }
}
