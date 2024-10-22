using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

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

                var check = await _context.Category.Select(c => new
                {
                    c.Id,
                    c.Name_Category,
                    c.Category_Description
                    // Исключаем Image_Category
                }) .ToListAsync();

                if (check == null && check.Count == 0)
                {
                    empty = false;
                }
            }
            catch (Exception)
            {

            }

            if (empty == true)
            {
                await foreach (var category in _context.Category.AsAsyncEnumerable())
                {
                    yield return category;
                }
            }

        }

        [HttpGet("all")]
        public async Task<ActionResult> CategoryAll()
        {
            try
            {
                //var Category =  _context.Category.Select(c => new
                //  {
                //      c.Id,
                //      c.Name_Category,
                //      c.Category_Description,
                //      Image_Category_Id = c.Image_Category.Id // Получаем только Id изображения

                //     //c.Image_Category. // Включаем изображение

                //    //Исключаем Image_Category
                //});
                var  Category = _context.Category.Include(u => u.Image_Category).ToList();

                /*await _context.Category.ToListAsync();*/
                if (Category == null)
                {
                    return StatusCode(404);
                }
                else
                {
                    return Ok(Category);

                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
        [HttpGet("fille{Id}")] 
        public async Task<ActionResult> Category_Fille(long Id)
        {
            try
            {
                //var Category = await _context.Category.Where(u=>u.Id == Id).Select(u=> u.Image_Category).FirstOrDefaultAsync();
                var Category = await _context.Category.FirstOrDefaultAsync(u=>u.Id ==Id);

                //var Category = await _context.Category.FirstOrDefaultAsync(u => u.Id == Id);
                if (Category == null)
                {
                    return StatusCode(404);
                }
                else
                {
                    return Ok(Category.Image_Category);

                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpGet("Id{Id}")]
        public async Task<ActionResult> CategoryId(long Id)
        {
            try
            {


                var Category = await _context.Category.FirstOrDefaultAsync(u => u.Id == Id);
                if (Category == null)
                {
                    return StatusCode(404);
                }
                else
                {
                    return Ok(Category);

                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }


        [HttpPut("put{id}")]
        public async Task<ActionResult> CategoryUpdate(long id, [FromBody] Category Category)
        {
            try
            {


                _context.Category.Update(Category);
                await _context.SaveChangesAsync();
                return Ok(Category);
            }
            catch (Exception ex)
            {
                return StatusCode(404);

            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CategoryCreate([FromBody] Category category)
        {
            try
            {
                category.Image_Category  = await _context.Image.Include(u =>u.ImageCopies).FirstOrDefaultAsync(U=> U.Id == category.Image_Category.Id);
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();

                    return Ok(new {message = "Category created successfully",

                        category.Id,
                        category.Category_Description,
                     
                        // Include other relevant properties
                    });
                //return Ok(new { message = "Category created successfully", category = category });
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> CategoryDelete(long id)
        {
            try
            {
                var product = await _context.Category.FirstOrDefaultAsync(u => u.Id == id);

                _context.Category.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(404);

            }
        }



    }
}
