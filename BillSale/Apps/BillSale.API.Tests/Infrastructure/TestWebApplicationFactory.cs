using BillSale.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace BillSale.API.Tests.Infrastructure
{
    /// <summary>
    /// Фабрика теста приложения
    /// </summary>
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

                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new InvalidOperationException(
                            "Для интеграционных тестов не задана строка подключения.");
                    }

                    var testConnection = new NpgsqlConnectionStringBuilder(connectionString)
                    {
                        Database = $"BillSaleTest_{Guid.NewGuid():N}"
                    };

                    var optionsBuilder = new DbContextOptionsBuilder<BillSaleContext>()
                        .UseApplicationServiceProvider(provider)
                        .UseNpgsql(testConnection.ConnectionString);

                    return optionsBuilder.Options;
                });
            });
        }
    }
}
