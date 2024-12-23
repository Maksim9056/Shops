
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using Store_Products.Service;

namespace Store_Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ProductService>();
            //builder.Services.AddScoped<ProductConsumer>();
            //builder.Services.AddScoped<ProductProducer>();
            //// ����������� ����� Kafka ��� Singleton
            //builder.Services.AddSingleton<ProductProducer>();
            //builder.Services.AddSingleton<ProductConsumer>();

            // ���������� �����������, ���� ��� ���������
            builder.Services.AddMemoryCache();

            builder.Services.AddDbContext<ShopData>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Shop")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); // ����-��� ��������� ����������
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5); // ����-��� ��� ����������
            });

            var redisConnectionString = builder.Configuration.GetSection("Redis")["ConnectionString"];
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "ProductCache_";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ShopData>();
                // ���������, ���� �� ������������� ��������
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
