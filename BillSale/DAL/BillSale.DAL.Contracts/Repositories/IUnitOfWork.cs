namespace BillSale.DAL.Contracts.Repositories
{
    /// <summary>
    /// Определяет интерфейс для Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения контекста
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
