using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Data;
using POS.DataLayer.DataAccess.userDAL;
using POS.Service.userServices;
using POS.Interface.interfaces;
using POS.system.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IUserDll, UserDll>();

builder.Services.AddDbContext<POSDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<exceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


