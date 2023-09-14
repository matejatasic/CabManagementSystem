using Microsoft.EntityFrameworkCore;
using CabManagementSystemWeb;
using CabManagementSystemWeb.Services;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Repositories;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<Employee>, EmployeesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

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
