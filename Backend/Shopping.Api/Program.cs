using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Data;
using Shopping.Api.Data.Repositories;
using Shopping.Api.Helpers;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Interfaces.IServices;
using Shopping.Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8
    .GetBytes(secretKey!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt => {
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       IssuerSigningKey = key
                   };
               });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
//repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
