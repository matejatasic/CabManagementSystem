using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using DotNet.Testcontainers.Builders;
using Testcontainers.PostgreSql;

namespace CabManagementSystemWeb.Tests;

public class CustomWebApplicationFactory<TProgram, TDbContext> :
WebApplicationFactory<TProgram>, IAsyncLifetime
where TProgram : class
where TDbContext : DbContext
{
    private readonly PostgreSqlContainer _container;

    public CustomWebApplicationFactory()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithHostname("localhost")
            .WithDatabase("cab-management-system-test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithExposedPort(5432)
            .WithPortBinding(5432, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services = this.RemoveDbContext<TDbContext>(services);

            services.AddDbContext<TDbContext>(options => {
                options.UseNpgsql(_container.GetConnectionString());
            });

            services = this.EnsureDbCreated<TDbContext>(services);
        });
    }

    public IServiceCollection RemoveDbContext<T>(IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null) services.Remove(descriptor);

        return services;
    }

    public IServiceCollection EnsureDbCreated<T>(IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<T>();
        context.Database.EnsureCreated();

        return services;
    }

    public async Task InitializeAsync()
    {
       await _container.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}

