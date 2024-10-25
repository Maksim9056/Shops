using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shops.Client.Pages.Admin.Category;
using Shops.Client;
using Shops.Client.Pages.Admin.Product;
using Shops.Client.Service;
using Shops.Client.Pages.Orders;
using ShopClassLibrary.Service; // Adjust the namespace as per your project

namespace Shops.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddLogging();
            //// Регистрация HttpClient
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //// Загрузка конфигурации
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Регистрация HttpClient
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Создаем HttpClient для загрузки appsettings.json
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            // Загружаем appsettings.json
            var response = await httpClient.GetAsync("appsettings.json");

            // Проверяем успешность запроса
            response.EnsureSuccessStatusCode();

            // Читаем поток данных
            using var stream = await response.Content.ReadAsStreamAsync();

            // Строим конфигурацию
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build(); 

            // Регистрируем конфигурацию
            builder.Services.AddSingleton<IConfiguration>(configuration);

            // Регистрируем ваши сервисы
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<JWT_Decode>();

            builder.Services.AddScoped<ImageService>();
            builder.Services.AddScoped<ProductService>(); 
            builder.Services.AddScoped<UrlService>(); 
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddBlazoredLocalStorage(); ;
            await builder.Build().RunAsync();
        }
    }
}
