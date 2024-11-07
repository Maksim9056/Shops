using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// �������� URL ������������ �����, ����� �������������� �������� �� appsettings.json
builder.WebHost.UseUrls("https://localhost:5000", "http://localhost:5001");

// ��������� ��������� �������
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

// ���������� Ocelot Middleware
await app.UseOcelot();

app.MapControllers();
app.Run();
