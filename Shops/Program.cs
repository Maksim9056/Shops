using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Shops.Client;
using Shops.Client.Pages;
using Shops.Client.Pages.Admin.Category;
using Shops.Client.Pages.Admin.Product;
using Shops.Client.Pages.Orders;
using Shops.Client.Service;
using Shops.Components;

namespace Shops
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddHttpClient();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Загружаем словарь URL-адресов
            var urls = builder.Configuration.GetSection("Urls").Get<Dictionary<string, string>>();
            builder.Services.AddLogging();
            builder.Services.AddScoped<UrlService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ImageService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddBlazoredLocalStorage();


            // Проверяем и выводим загруженные данные
            if (urls != null)
            {
                foreach (var item in urls)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("URLs not found in the configuration.");
            }
            builder.Services.AddSingleton(builder.Configuration);
            builder.Services.AddLogging();  // For Blazor Server
            builder.Services.AddBlazoredLocalStorage();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            //app.MapRazorComponents<App>()
            // .AddInteractiveServerRenderMode(); // Уберите WebAssembly render mode

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly)
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
