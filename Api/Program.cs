using System.Text;
using Api.Data;
using Api.Data.Repositories;
using Api.Filters;
using Api.Helpers;
using Api.Interface;
using Api.Middleware;
using Api.Services;
using Api.Services.Interface;
using Api.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddCors();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogUserActivity>();
});
 builder.Services.AddFluentValidationAutoValidation();
 builder.Services.AddValidatorsFromAssemblyContaining<Program>();
 builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPhotoService,PhotoService>();
builder.Services.AddScoped<IMemberRepo, MemberRepo>();
builder.Services.AddScoped<IMessageRepo, MessageRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ILikesRepo, LikesRepo>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.Configure<CloudinarySettting>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSignalR();
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Cannot get Token key");
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience=false
    };
    option.Events=new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if(context.Exception is SecurityTokenExpiredException)
            {
                context.Response.Headers.Append("Token-Expired","True");
            }
             return Task.CompletedTask;
        }
   

    };
    option.Events=new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
          var accessToken=context.Request.Query["access_token"];
          var path=context.HttpContext.Request.Path;
          if(!string.IsNullOrWhiteSpace(accessToken)&&path.StartsWithSegments("/hubs"))
            {
                context.Token=accessToken;
            }
            return Task.CompletedTask;
        }
        
    };
    

}
 );
 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/Messages");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();            // ✅ Apply migrations
        await SeedData.SeedDataFromFile(context);         // ✅ Seed your users/members/photos
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration or seeding");
    }
}
app.Run();
