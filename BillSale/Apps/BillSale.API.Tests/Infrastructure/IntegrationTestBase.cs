using BillSale.DAL.Context;
using BillSale.DAL.Contracts.Repositories;


namespace BillSale.API.Tests.Infrastructure;

/// <summary>
/// Базовый класс для интеграционных тестов с авторизацией.
/// Предоставляет доступ к БД, UnitOfWork, конфигурации и хелперы для создания
/// тестовых пользователей и авторизованных HTTP-клиентов.
/// Каждый вызов хелперов создаёт свежие сущности и клиентов — без мутации общего состояния.
/// </summary>
[Collection(nameof(BillsalseApiTestCollection))]
public abstract class IntegrationTestBase
{
    private readonly BillSaleApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="IntegrationTestBase"/>.
    /// </summary>
    protected IntegrationTestBase(BillSaleApiFixture fixture)
    {
        this.fixture = fixture;
        Context = fixture.Context;
        UnitOfWork = fixture.UnitOfWork;
    }

    /// <summary>
    /// Контекст БД из DI-контейнера.
    /// </summary>
    protected BillSaleContext Context { get; }

    /// <summary>
    /// UnitOfWork для сохранения изменений через контекст БД.
    /// </summary>
    protected IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Создаёт анонимный (неавторизованный) HttpClient — свежий экземпляр на каждый вызов.
    /// </summary>
    protected HttpClient CreateClient() => fixture.CreateClient();
}
