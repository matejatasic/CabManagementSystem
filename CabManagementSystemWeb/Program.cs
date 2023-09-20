using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CabManagementSystemWeb;
using CabManagementSystemWeb.Services;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Repositories;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);
builder.Services.AddScoped<IRepository<Employee>, EmployeesRepository>();
builder.Services.AddScoped<IRepository<Branch>, BranchesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IBranchesService, BranchesService>();

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
