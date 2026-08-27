using BillSale.DAL.Contracts.Interfaces;

namespace BillSale.DAL.Contracts.Repositories
{
    /// <summary>
    /// Интерфейс создания и модификации записей в контексте
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Изменить запись
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;


        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;

    }
}
