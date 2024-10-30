using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
if (string.IsNullOrEmpty(postgresConnectionString))
{
    throw new Exception("Invalid PostgreSQL Connection String");
}
builder.Services.AddDbContext<BookExchangeContext>(options => options.UseNpgsql(postgresConnectionString));
builder.Services.AddScoped<BookOfferingsRepository>();
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<NotificationsRepository>();
builder.Services.AddScoped<SavedBooksRepository>();
builder.Services.AddScoped<SchoolsRepository>();
builder.Services.AddScoped<StudentsRepository>();

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

//app.UseHttpsRedirection(); TODO: uncomment

app.UseAuthorization();

app.MapControllers();

app.Run();
