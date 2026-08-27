using BillSale.DAL.Contracts.Interfaces;

namespace BillSale.DAL.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс получение записей из контекста
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary>
        IQueryable<IEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
