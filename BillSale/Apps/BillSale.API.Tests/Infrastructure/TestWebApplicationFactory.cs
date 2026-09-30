using BillSale.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BillSale.API.Tests.Infrastructure
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<BillSaleContext>>();

                services.AddSingleton<DbContextOptions<BillSaleContext>>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    var dbConectionOptions = new DbContextOptions<BillSaleContext>(
                        new Dictionary<Type, IDbContextOptionsExtension>());
                    var optionsBuilder = new DbContextOptionsBuilder<BillSaleContext>(dbConectionOptions)
                        .UseApplicationServiceProvider(provider)
                        .UseNpgsql(string.Format(connectionString, Guid.NewGuid().ToString("N")));

                    return optionsBuilder.Options;
                });
            });
        }
    }
}
