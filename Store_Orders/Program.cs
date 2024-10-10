
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;

namespace Store_Orders
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ShopData>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("Shop")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ShopData>();
                // ѕровер€ем, есть ли неприемленные миграции
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
