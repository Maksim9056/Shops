using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Получить все заказы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders
                .Include(o => o.Idproduct)
                .Include(o => o.User)
                .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Получить все заказы для конкретного пользователя по UserId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(long userId)
        {
            try
            {

                var orders = await _context.Orders
                    .Where(o => o.User.Id == userId)
                    .Include(o => o.Idproduct)
                    .Include(o => o.User)
                    .ToListAsync();

                if (orders == null || !orders.Any())
                {
                    return NotFound($"Заказы для пользователя с ID {userId} не найдены.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // Получить заказ по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(long id)
        {
            try
            {
                var order = await _context.Orders
                .Include(o => o.Idproduct)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Создать новый заказ
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Id_User_Image.ImageCopies).Include(U => U.Status).FirstOrDefaultAsync(U => U.Id == order.User.Id);

                //var productImage = await _context.Image.Include(u => u.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Id_ProductDataImage.Id);
           
                
                var productStatus = await _context.Status.FirstOrDefaultAsync(U => U.Id == order.Status.Id);
               List<Product> orders = new List<Product>();

                for (int i = 0; i < order.Idproduct.Count(); i++)
                {
                    var CategoryImage = await _context.Products.Include(u => u.Id_ProductDataImage.ImageCopies).Include(U=>U.Status).Include(u=>u.Category_Id.Image_Category.ImageCopies).FirstOrDefaultAsync(U => U.Id == order.Idproduct.ElementAt(i).Id);
                    orders.Add(CategoryImage);
                }
                if (user != null)
                {// Если статус существует, присоединяем его к контексту



                    _context.Entry(user).State = EntityState.Unchanged;
                    order.User = user;
                    _context.Entry(productStatus).State = EntityState.Unchanged;

                    order.Status = productStatus;
                    //_context.Entry(orders).State = EntityState.Unchanged;
                    order.Idproduct = orders;
                }

                    //Parallel.ForEach(order., product =>
                    //{
                    //    //if (product.Id_ProductDataImage != null)
                    //    //{
                    //    product.Id_ProductDataImage.OriginalImageData = new byte[0];
                    //    //}

                    //    //if (product.Category_Id?.Image_Category != null)
                    //    //{
                    //    product.Category_Id.Image_Category.OriginalImageData = new byte[0];
                    //    //}
                    //});

                    //for (int i = 0; i < order.Idproduct.Count(); i++)
                    //{
                    //    var CategoryImage = await _context.Category.Include(u => u.Image_Category.ImageCopies).FirstOrDefaultAsync(U => U.Id == order.Idproduct[i].Category_Id.Id);

                    //}
                    //var productImage = await _context.Image.Include(u => u.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Id_ProductDataImage.Id);
                    //if (productImage == null)
                    //{
                    //    return BadRequest("Invalid Image data.");
                    //}

                    //var productStatus = await _context.Status.FirstOrDefaultAsync(U => U.Id == product.Status.Id);
                    //if (productStatus == null)
                    //{
                    //    return BadRequest("Invalid Status data.");
                    //}

                    //if (productImage != null)
                    //{// Если статус существует, присоединяем его к контексту



                    //    _context.Entry(productImage).State = EntityState.Unchanged;
                    //    product.Id_ProductDataImage = productImage;
                    //    _context.Entry(productStatus).State = EntityState.Unchanged;

                    //    product.Status = productStatus;
                    //    _context.Entry(CategoryImage).State = EntityState.Unchanged;
                    //    product.Category_Id = CategoryImage;

                    //}


                    //if (order == null)
                    //{
                    //    return BadRequest();
                    //}

                    _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return Ok( new { id = order.Id });

                //return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Редактировать существующий заказ
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] Order updatedOrder)
        {
            try
            {
                if (id != updatedOrder.Id)
                {
                    return BadRequest();
                }

                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                order.OrdersName = updatedOrder.OrdersName;
                order.Idproduct = updatedOrder.Idproduct;
                order.User = updatedOrder.User;
                order.Status = updatedOrder.Status;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Удалить заказ
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
