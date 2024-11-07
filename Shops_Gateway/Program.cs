using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Добавьте URL конфигурацию здесь, чтобы переопределить значение из appsettings.json
builder.WebHost.UseUrls("https://localhost:5000", "http://localhost:5001");

// Добавляем остальные сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Используем Ocelot Middleware
await app.UseOcelot();

app.MapControllers();
app.Run();
