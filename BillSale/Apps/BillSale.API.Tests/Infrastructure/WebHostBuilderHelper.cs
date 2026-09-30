using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BillSale.API.Tests.Infrastructure
{
    internal static class WebHostBuilderHelper
    {
        internal static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
        {
            builder.UseEnvironment("integration");
            builder.ConfigureAppConfiguration((_, config) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.integration.json");
                config.AddJsonFile(configPath).AddEnvironmentVariables();
            });
        }
    }
}
