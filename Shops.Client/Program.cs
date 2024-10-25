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
            //// ����������� HttpClient
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //// �������� ������������
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // ����������� HttpClient
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // ������� HttpClient ��� �������� appsettings.json
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            // ��������� appsettings.json
            var response = await httpClient.GetAsync("appsettings.json");

            // ��������� ���������� �������
            response.EnsureSuccessStatusCode();

            // ������ ����� ������
            using var stream = await response.Content.ReadAsStreamAsync();

            // ������ ������������
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build(); 

            // ������������ ������������
            builder.Services.AddSingleton<IConfiguration>(configuration);

            // ������������ ���� �������
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
