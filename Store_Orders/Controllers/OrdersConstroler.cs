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
                var user =  await _context.Users.Include(u  =>u.Status).Include(u => u.Id_User_Image).FirstOrDefaultAsync(u => u.Id == userId);
                var orders =  _context.Orders
                    .Include(o => o.User).Include(o => o.Status)
                    .Where(o => o.User == user).ToList();

                Parallel.ForEach(orders, product =>
                {
                    //if (product.Id_ProductDataImage != null)
                    //{
                    product.User.Id_User_Image.OriginalImageData = new byte[0];
                    //}
                    product.User.Id_User_Image.ImageCopies = new List<ImageCopy>();
                 //if (product.Category_Id?.Image_Category != null)
                 //{
                 product.User.Orders = new List<Order>();
                    //}
                });

                if (orders == null || !orders.Any())
                {
                    return NotFound($"Заказы для пользователя с ID {userId} не найдены.");
                }
                //var a = orders.ToList();
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

        // 
        [HttpPost("pay")]
        public async Task<ActionResult<string>> Order([FromBody] Advance_Payment order)
        {
            try
            {

                var user = await _context.Users
                .Include(u => u.Id_User_Image)
                    .ThenInclude(i => i.ImageCopies)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.Id == order.Id_User);
              long Orders_product=0;

                // Извлекаем заказы, которые необходимо оплатить
                var ordersToPay = await _context.Orders.Include(u => u.Status)
                    .Where(o =>  order.SelectedOrderIds.Contains(o.Idproduct) && o.User.Id == user.Id)
                    .ToListAsync();




                if (ordersToPay.Count != order.SelectedOrderIds.Count)
                {
                    return BadRequest("Некоторые заказы не найдены или не принадлежат пользователю.");
                }
                long  SUMM = 0;
                foreach( var i in order.SelectedOrderIds)
                {
                    
                    var prodduct = await _context.Products.FirstOrDefaultAsync(u => u.Id == i);
                    if(prodduct != null)
                    {
                        SUMM = +prodduct.ProductPrice;

                    }
                }

                // Рассчитываем общую стоимость заказов
                var totalAmount = SUMM;

                // Проверяем, достаточно ли средств на счету
                if (user.Money_Account < totalAmount)
                {
                    return BadRequest("Недостаточно средств для оплаты заказов.");
                }

                // Списываем сумму со счета пользователя и обновляем статус заказов
                user.Money_Account -= totalAmount;
                foreach (var orders in ordersToPay)
                {
                    orders.Status = await _context.Status.FirstOrDefaultAsync(s => s.Description == "Заказ оплачен!");
                }

                // Сохраняем изменения
                await _context.SaveChangesAsync();

                return Ok("Оплата прошла успешно.");
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

                var user = await _context.Users
             .Include(u => u.Id_User_Image)
                 .ThenInclude(i => i.ImageCopies)
             .Include(u => u.Status)
             .FirstOrDefaultAsync(u => u.Id == order.User.Id);

                var orderqqo =  _context.Orders
                    .Include(o => o.User)
                    .Where(o => o.Idproduct == order.Idproduct && o.User == user);

                bool orderExists = await _context.Orders
                 .AnyAsync(o => o.Idproduct == order.Idproduct && o.User.Id == user.Id);
                if (orderExists == false)
                {
                    var productStatus = await _context.Status.FirstOrDefaultAsync(U => U.Id == order.Status.Id);
                    //List<Product> orders = new List<Product>();

                    //for (int i = 0; i < order.Idproduct.Count(); i++)
                    //{
                    //    var CategoryImage = await _context.Products.Include(u => u.Id_ProductDataImage.ImageCopies).Include(U => U.Status).Include(u => u.Category_Id.Image_Category.ImageCopies).FirstOrDefaultAsync(U => U.Id == order.Idproduct.ElementAt(i).Id);
                    //    orders.Add(CategoryImage);
                    //}

                    if (user != null)
                    {// Если статус существует, присоединяем его к контексту



                        _context.Entry(user).State = EntityState.Unchanged;
                        order.User = user;
                        _context.Entry(productStatus).State = EntityState.Unchanged;

                        order.Status = productStatus;
                        //_context.Entry(orders).State = EntityState.Unchanged;
                        //order.Idproduct = orders;
                    }





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


                    if (order == null)
                    {
                        return BadRequest();
                    }

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    return Ok(new { id = order.Id });
                    // Логика добавления товара, если он еще не добавлен  
                 

                }
                else
                {
                    // Товар уже добавлен пользователем
                    Console.WriteLine("Товар уже добавлен.");
                    return BadRequest("Товар уже добавлен у этого пользователя." + $"{user.Surname}, {user.UserName} ,{user.Email} ");
                }


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
