using System.Diagnostics;
using BillSale.DAL.Context;
using BillSale.DAL.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace BillSale.API.Tests.Infrastructure
{
    /// <summary>
    /// Базовая фикстура для интеграционных тестов Oriven API.
    /// Поднимает полный ASP.NET pipeline с реальным PostgreSQL (уникальная БД на фикстуру).
    /// </summary>  
    public class BillSaleApiFixture : IAsyncLifetime
    {
        private bool disposed;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BillSaleApiFixture"/>
        /// </summary>
        public BillSaleApiFixture()
        {
            Factory = new TestWebApplicationFactory();
        }

        /// <summary>
        /// Фабрика тестового веб-приложения
        /// </summary>
        protected TestWebApplicationFactory Factory { get; }

        /// <summary>
        /// Контекст БД из DI-контейнера
        /// </summary>
        internal BillSaleContext Context
        {
            get
            {
                if (field != null)
                {
                    return field;
                }

                var scope = Factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                field = scope.ServiceProvider.GetRequiredService<BillSaleContext>();
                return field;
            }
        }

        /// <summary>
        /// UnitOfWork для сохранения изменений
        /// </summary>
        internal IUnitOfWork UnitOfWork => Context;

        /// <summary>
        /// Провайдер сервисов DI-контейнера для доступа к IConfiguration и др.
        /// </summary>
        internal IServiceProvider Services => Factory.Services;

        /// <summary>
        /// Создаёт анонимного (неавторизованного) HTTP-клиента
        /// </summary>
        internal HttpClient CreateClient() => Factory.CreateClient();

        /// <inheritdoc />
        public virtual async ValueTask InitializeAsync()
        {
            await Context.Database.MigrateAsync();
            var assembly = typeof(BillSaleContext).Assembly;

            Debug.WriteLine(assembly.FullName);

            var migrationTypes = assembly.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Migration)));

            foreach (var type in migrationTypes)
            {
                Debug.WriteLine(type.FullName);
            }
        }

        /// <inheritdoc />
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.CloseConnectionAsync();
            await Context.DisposeAsync();
            await Factory.DisposeAsync();
        }
    }
}
