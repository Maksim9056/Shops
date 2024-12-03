using Microsoft.AspNetCore.Components;
using ShopClassLibrary.ModelShop;
using Shops.Client.Pages.Admin.Category;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Shops.Client.Pages.Admin.Product
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlService _apiKey;
        public readonly string _url;
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
            return await _httpClient.GetFromJsonAsync<ShopClassLibrary.ModelShop.Product>(_url + $"/ProductControler/{id}");
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

        public async Task<List<ShopClassLibrary.ModelShop.Product>> SearchProductsAsync(string searchQuery, string selectedCategory, long minPrice, long maxPrice)
        {
            // Replace with actual search logic, e.g., a call to an API endpoint
            var response = await _httpClient.GetFromJsonAsync<List<ShopClassLibrary.ModelShop.Product >>(_url+$"/ProductControler/search?query={searchQuery}&category={selectedCategory}&minPrice={minPrice}&maxPrice={maxPrice}");
            return response ?? new List<ShopClassLibrary.ModelShop.Product>();
        }


        public async Task UpdateProductAsync(ShopClassLibrary.ModelShop.Product product)
        {
            product.Category_Id.Image_Category.ImageCopies = new List<ImageCopy>();
            product.Id_ProductDataImage.ImageCopies = new List<ImageCopy>();
            await _httpClient.PutAsJsonAsync(_url + $"/ProductControler/update{product.Id}", product);
        }

        public async Task DeleteProductAsync(long id)
        {
            await _httpClient.DeleteAsync(_url + $"/ProductControler/delete/{id}");
        }
    }

}
