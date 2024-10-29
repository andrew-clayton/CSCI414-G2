using BookExchangeApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
if (string.IsNullOrEmpty(postgresConnectionString))
{
    throw new Exception("Invalid PostgreSQL Connection String");
}
builder.Services.AddDbContext<BookExchangeContext>(options => options.UseNpgsql(postgresConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
