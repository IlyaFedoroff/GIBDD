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




// ��������� ������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://localhost:4200") // ��������� ������� � localhost:4200
              .AllowAnyHeader() // ��������� ����� ���������
              .AllowAnyMethod(); // ��������� ����� HTTP-������ (GET, POST, PUT � �.�.)
    });
});



var app = builder.Build();


// �������� ������������� CORS
app.UseCors("AllowAngularApp");




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");



app.Run();
