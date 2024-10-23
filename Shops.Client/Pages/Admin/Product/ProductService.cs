using Microsoft.AspNetCore.Components;
using Shops.Client.Pages.Admin.Category;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Shops.Client.Pages.Admin.Product
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        private readonly string _url;
        public ProductService(HttpClient httpClient, UrlService apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _url = _apiKey.GetUserStore_Products_ServiceUrl();
        }
        //CategoryId

        public async Task<List<ShopClassLibrary.ModelShop.Product>> GetProductsAsyncCategoryId(long Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ShopClassLibrary.ModelShop.Product>>(_url + $"/ProductControler/CategoryId{Id}");

            }
            catch (Exception ex)
            {
                return new List<ShopClassLibrary.ModelShop.Product>();
            }
        }

        public async Task<List<ShopClassLibrary.ModelShop.Product>> GetProductsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ShopClassLibrary.ModelShop.Product>>(_url + "/ProductControler");

            }catch (Exception ex)
            {
                return   new List<ShopClassLibrary.ModelShop.Product>();
            }
        }

        public async Task<ShopClassLibrary.ModelShop.Product> GetProductByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<ShopClassLibrary.ModelShop.Product>(_url + $"ProductControler/{id}");
        }

        public async Task<ShopClassLibrary.ModelShop.Product> CreateProductAsync(ShopClassLibrary.ModelShop.Product product)
        {
            try
            {

                var productResponse = await _httpClient.PostAsJsonAsync(_url + "/ProductControler/create", product);

                if (productResponse.IsSuccessStatusCode)
                {
                    var createdCategory = await productResponse.Content.ReadFromJsonAsync<ShopClassLibrary.ModelShop.Product>();
                    Console.WriteLine($"Category created: {createdCategory.Name_Product}");
                    return createdCategory;
                }
                else
                {
                    var errorMessage = await productResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error creating category: {errorMessage}");
                    return new ShopClassLibrary.ModelShop.Product();

                }
            }
            catch (Exception ex)
            {
                return new ShopClassLibrary.ModelShop.Product();
                Console.WriteLine(ex.Message);
            }
        }


        public async Task UpdateProductAsync(ShopClassLibrary.ModelShop.Product product)
        {
            await _httpClient.PutAsJsonAsync(_url + $"ProductControler/{product.Id}", product);
        }

        public async Task DeleteProductAsync(long id)
        {
            await _httpClient.DeleteAsync(_url + $"ProductControlers/{id}");
        }
    }

}
