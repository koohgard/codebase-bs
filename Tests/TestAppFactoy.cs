using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class TestAppFactoy : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }
        });
        builder.UseEnvironment("Test");
    }
    public void ResetDatabase()
    {
        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}



