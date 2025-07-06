using Microsoft.EntityFrameworkCore;
using ASP.NetCoreWebAPICRUD.Models;
//ye upar dono folder hai aur inke naam MyDbContext files me hai toh iss file me use karne ke liye apne DB ko ye dono line yaha add ki hai


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ye script MyDBContext class file and sql server ko yaha add kar degi kyunki ye file program.cs hi sabse pahle execute hoti hai
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<MyDbContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
