using AutoManager.Cache;
using AutoManager.Data;
using AutoManager.Extension;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mysql
var mysqlConnectionString = builder.Configuration.GetConnectionString("MysqlDatabase");
if(string.IsNullOrEmpty(mysqlConnectionString))
{
    throw new InvalidOperationException("A connection string MYSQL nao esta configurada");
}
builder.Services.AddDbContext<AutoManagerContext>(options => options.UseMySQL(mysqlConnectionString));

//Redis
var redisConnectionString = builder.Configuration.GetConnectionString("RedisCache");
if (string.IsNullOrEmpty(mysqlConnectionString))
{
    throw new InvalidOperationException("A connection string REDIS nao esta configurada");
}
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);

builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.ApplyMigrations();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
