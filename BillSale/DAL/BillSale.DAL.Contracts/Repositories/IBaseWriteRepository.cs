using BillSale.DAL.Contracts.Interfaces;

namespace BillSale.DAL.Contracts.Repositories
{
    /// <summary>
    /// Базовый интерфейс репозитория для операций записи
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IBaseWriteRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="Entity">сущность</param>
        void Add(T Entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">сущность</param>
        void Update(T entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">сущность</param>
        void Delete(T entity);
    }
}
