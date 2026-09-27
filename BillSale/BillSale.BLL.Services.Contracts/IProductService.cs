using BillSale.BLL.Services.Contracts.Models.Product;

namespace BillSale.BLL.Services.Contracts
{
    /// <summary>
    /// Контракт сервиса сущности продукт
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Список продуктов</returns>
        Task<IReadOnlyCollection<ProductModel>> GetProductsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Продукт</returns>
        Task<ProductModel> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление продукта
        /// </summary>
        /// <param name="productModel">Продукт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<ProductModel> AddProductAsync(ProductCreateModel productModel, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление продукта
        /// </summary>
        /// <param name="productModel">Продукт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task UpdateProductAsync(ProductUpdateModel productModel, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}
