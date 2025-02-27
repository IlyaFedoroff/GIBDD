using GIBDD.Server.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("http://localhost", "http://gibdd-angular:80", "https://localhost", "http://gibdd-angular", "http://localhost:4200") // Разрешаем доступ только с указанного источника
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // пробуем подключиться к базе данных несколько раз
    for (int i = 0; i < 10; i++) {
        try
        {
            await dbContext.Database.MigrateAsync();
            Console.WriteLine($"Удалось подключиться к БД ({i + 1}/10).");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось подключться к БД ({i + 1}/10). Ошибка: {ex.Message}");
            await Task.Delay(5000); // ждем 5 секунд перед повторной попыткой
        }
    }
}


// Применение CORS политики

app.UseCors("AllowSpecificOrigin");  // Применяем политику



app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseWebSockets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();


app.MapGet("/", () => "Hello World!");

app.UseAuthorization();
app.MapControllers();

//app.MapFallbackToFile("/index.html");



app.Run();
