using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;

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

        [HttpGet]
        public async IAsyncEnumerable<Category> Category()
        {
            bool empty = true;
            try
            {
                var  check = await  _context.Category.ToListAsync();

                if(check == null && check.Count ==0)
                {
                    empty = false;
                }
            }
            catch(Exception)
            {

            }
            
            if(empty == true)
            {
                await foreach (var category in _context.Category.AsAsyncEnumerable())
                {
                    yield return category;
                }
            }
         
        }

        [HttpPost]
        public async Task CategoryCreate([FromBody] Category Category)
        {

        }



    }
}
