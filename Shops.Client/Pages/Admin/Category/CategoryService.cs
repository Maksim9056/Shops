using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Shops.Client.Pages.Admin.Category
{
    public class CategoryService
    {
        private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;
        private readonly UrlService _urlService;
        private readonly HttpClient _httpClient;
        public readonly string Url_Product;
        public  CategoryService(HttpClient httpClient, Blazored.LocalStorage.ILocalStorageService localStorage, UrlService urlService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _urlService = urlService;
          Url_Product = _urlService.GetUserStore_Category_ServiceUrl();

        }

        public async Task<List<ShopClassLibrary.ModelShop.Category>> GetCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ShopClassLibrary.ModelShop.Category>>(Url_Product + $"/CategoryControler/all");
            }catch(Exception)
            {
                return new List<ShopClassLibrary.ModelShop.Category>();

            }
        }

        //public async Task<List<ShopClassLibrary.ModelShop.Category>> GetAllCategoriesAsync()
        //{
        //    try
        //    {

        //        //return 
        //    }
        //    catch (Exception ex)
        //    {
        //        List<ShopClassLibrary.ModelShop.Category> categories = new List<ShopClassLibrary.ModelShop.Category>();


        //        return categories;
        //    }
        //}

        public async Task<ShopClassLibrary.ModelShop.Category> GetCategoryByIdAsync(long id)
        {
            try
            {

                return await _httpClient.GetFromJsonAsync<ShopClassLibrary.ModelShop.Category>(Url_Product + $"/CategoryControler/Id{id}");
            }
            catch (Exception ex)
            {
                ShopClassLibrary.ModelShop.Category s = new ShopClassLibrary.ModelShop.Category();
                return s;
            }
        }



        public async Task UpdateCategoryAsync(ShopClassLibrary.ModelShop.Category category)
        {
            try
            {


                await _httpClient.PutAsJsonAsync(Url_Product + $"/CategoryControler/put{category.Id}", category);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteCategoryAsync(long id)
        {
            try
            {

                await _httpClient.DeleteAsync(Url_Product + $"/CategoryControler/{id}");
            }
            catch (Exception ex)
            {

            }
        }
    }

}
