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
                return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>(_url + "/OrdersConstroler");
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }

        public async Task<string> Post_AllOrdersAsync(Advance_Payment orders, HashSet<long> selectedOrderIds )
        {
            try
            {
                // Отправляем POST-запрос с заказом
                var response = await _httpClient.PostAsJsonAsync(_url + "/OrdersConstroler/pay", orders);

                if (response.IsSuccessStatusCode)
                {
                    // Если запрос успешен, десериализуем созданный заказ и выводим ID
                    var createdOrder = await response.Content.ReadAsStringAsync();
                    if (createdOrder != null)
                    {
                        string successMessage = $"Заказ успешно создан с ID: {createdOrder}";
                        Console.WriteLine(successMessage);
                        foreach (var item in orders.SelectedOrderIds)
                        {
                            selectedOrderIds.Remove(item);

                        }

                        return successMessage;
                    }
                    else
                    {
                        string emptyContentMessage = "Заказ создан, но ответ сервера пустой.";
                        Console.WriteLine(emptyContentMessage);
                        return emptyContentMessage;
                    }
                }
                else
                {
                    // Если запрос не успешен, выводим сообщение об ошибке
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    string failureMessage = $"Ошибка при создании заказа: {errorMessage}";
                    Console.WriteLine(failureMessage);
                    return failureMessage;
                }
            }
            catch (Exception ex)
            {
                // Логируем исключение в случае ошибки и возвращаем сообщение для пользователя
                string exceptionMessage = $"Произошла ошибка при создании заказа: {ex.Message}";
                Console.WriteLine(exceptionMessage);
                return exceptionMessage;
            }
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(long userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Order>>(_url + $"/OrdersConstroler/user/{userId}");
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
