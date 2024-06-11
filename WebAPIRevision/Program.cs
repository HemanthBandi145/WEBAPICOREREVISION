using Microsoft.EntityFrameworkCore;
using WebAPIRevision.Data;
using WebAPIRevision.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ECommerceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}/{id?}"
    );

app.Run();
