using ShopClassLibrary.ModelShop;
using ShopClassLibrary.Service;
using System.Net.Http.Json;

namespace Shops.Client.Pages.Orders
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        private readonly string _url;
        public OrderService(HttpClient httpClient, UrlService apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _url = _apiKey.GetStore_Orders_ServiceUrl();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>(_url + "OrdersConstroler");
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(long userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>(_url + $"/OrdersConstroler/user/{userId}");
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }

        public async Task<Order> GetOrderByIdAsync(long id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Order>(_url + $"/OrdersConstroler/{id}");
            }
            catch (Exception ex)
            {
                return new Order();
            }
        }

        public async Task CreateOrderAsync(Order order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_url + "/OrdersConstroler", order);
                if (response.IsSuccessStatusCode)
                {
                    var createdCategory = await response.Content.ReadFromJsonAsync<ShopClassLibrary.ModelShop.Order>();
                    Console.WriteLine($"Category created: {createdCategory.Id}");
                    //return createdCategory;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error creating category: {errorMessage}");
                    ////return new ShopClassLibrary.ModelShop.Product();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {

                var response = await _httpClient.PutAsJsonAsync(_url + $"/OrdersConstroler/{order.Id}", order);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteOrderAsync(long id)
        {
            try
            {

                var response = await _httpClient.DeleteAsync(_url + $"/OrdersConstroler/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
