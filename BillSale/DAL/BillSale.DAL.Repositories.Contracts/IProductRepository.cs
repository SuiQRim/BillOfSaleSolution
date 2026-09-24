using BillSale.DAL.Contracts.Repositories;
using BillSale.Entities;

namespace BillSale.DAL.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий продуктовы <see cref="Product"/>
    /// </summary>
    public interface IProductRepository : IBaseWriteRepository<Product>
    {
        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<IReadOnlyCollection<Product>> GetProductsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);

    }
}
