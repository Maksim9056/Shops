
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopClassLibrary;

namespace StoreImage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ShopData>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Shop")));
            builder.Services.AddHttpClient();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<ImageService>();

            //services.AddControllers();
            builder.Services.AddControllers();
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); // Тайм-аут удержания соединения
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5); // Тайм-аут для заголовков
            });


            //builder.WebHost.ConfigureKestrel(serverOptions =>
            //{
            //    serverOptions.Limits.MaxRequestBodySize = 1024 * 1024 * 100; // 100 MB

            //    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(15); // Time allowed for request headers to be received

            //    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(15); // Keep connection alive for 5 minutes

            //    serverOptions.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 50, gracePeriod: TimeSpan.FromSeconds(3)); // More flexible for slower connections
            //});

            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1024 * 1024 * 100; // 100 MB
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ShopData>();
                // Проверяем, есть ли неприемленные миграции
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
                //dbContext.Database.Migrate(); 
            }

            app.MapControllers();

            app.Run();
        }
    }
}
