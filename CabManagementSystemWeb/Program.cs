using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using CabManagementSystemWeb;
using CabManagementSystemWeb.Services;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Repositories;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CabManagementSystemWeb.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

string AllowedOrigin = "Localhost origin";
builder.Services.AddCors(options => {
    options.AddPolicy(
        name: AllowedOrigin,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:8081")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowAnyHeader();
        }
    );
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);

builder.Services.AddScoped<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>, EmployeesRepository>();
builder.Services.AddScoped<IRepository<Branch, BranchCreateDto, BranchDetailDto>, BranchesRepository>();
builder.Services.AddScoped<IRepository<Car, CarCreateDto, CarDetailDto>, CarsRepository>();
builder.Services.AddScoped<IRepository<Route, RouteCreateDto, RouteDetailDto>, RoutesRepository>();
builder.Services.AddScoped<IRepository<User, UserCreateDto, UserDetailDto>, UsersRepository>();

builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IBranchesService, BranchesService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IRoutesService, RoutesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IHashService, HashService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(AllowedOrigin);
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
