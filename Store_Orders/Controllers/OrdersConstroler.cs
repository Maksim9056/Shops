using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using Store_Orders.Service;

namespace Store_Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersConstroler : ControllerBase
    {
        private readonly ShopData _context;
        private readonly OrderService _orderService;
        private readonly UserService _userService;
        public OrdersConstroler(ShopData context,OrderService orderService, UserService userService)
        {
            _context = context;
            _orderService = orderService;
            _userService = userService;
        }

        // Получить все заказы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {

            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                return HandleException(ex);
            }

        }

       
        // Получить все заказы для конкретного пользователя по UserId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(long userId)
        {
            try
            {
                var orders = await _userService.GetUserWithDetailsAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        // Получить заказ по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(long id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return order == null ? NotFound() : Ok(order);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // 
        [HttpPost("pay")]
        public async Task<ActionResult<string>> Order([FromBody] Advance_Payment order)
        {
            try
            {
                var result = await _orderService.ProcessOrderPaymentAsync(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

         // Создать новый заказ
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // Редактировать существующий заказ
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] Order updatedOrder)
        {
            try
            {
                var result = await _orderService.UpdateOrderAsync(id, updatedOrder);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // Удалить заказ
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private ActionResult HandleException(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
