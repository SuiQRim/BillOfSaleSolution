using System.Reflection;
using BillSale.API.Controllers;
using BillSale.API.Tests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BillSale.API.Tests
{
    /// <summary>
    /// Тесты зависимостей
    /// </summary>
    public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory">фабрика</param>
        public DependenciesTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder => builder.ConfigureTestAppConfiguration());
        }

        /// <summary>
        /// Проверка резолва зависимостей контроллеров из DI-контейнера
        /// </summary>
        [Theory]
        [MemberData(nameof(Controllers))]
        public void ControllerCoreShouldBeResolved(Type controller)
        {
            using var scope = factory.Services.CreateScope();
            var instance = scope.ServiceProvider.GetRequiredService(controller);
            Assert.NotNull(instance);
        }

        /// <summary>
        /// Контроллеры
        /// </summary>
        public static TheoryData<Type> Controllers => GetControllers<ProductController>();

        private static TheoryData<Type> GetControllers<TController>() =>
            new(Assembly.GetAssembly(typeof(TController))
                    ?.DefinedTypes
                    .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract)
                ?? []);

    }
}
