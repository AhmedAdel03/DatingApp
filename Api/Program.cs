using Api.Data;
using Api.Interface;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();

app.Run();
