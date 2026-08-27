using BillSale.DAL.Contracts.Interfaces;
using BillSale.DAL.Contracts.Repositories;

namespace BillSale.DAL.Context.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseWriteRepository<T> : IBaseWriteRepository<T>
        where T : class, IEntity
    {
        private readonly IDbWriterContext writerContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{T}"/>
        /// </summary>
        public BaseWriteRepository(IDbWriterContext writerContext)
        {
            this.writerContext = writerContext;
        }

        /// <inheritdoc/>
        public void Add(T entity)
        {
            if (entity is IEntityWithId entityWithId &&
                entityWithId.Id == Guid.Empty)

            {
                entityWithId.Id = Guid.NewGuid();
            }

            AuditForCreate(entity);
            AuditForUpdate(entity);
            writerContext.Writer.Add(entity);
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            AuditForUpdate(entity);
            writerContext.Writer.Update(entity);
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            if (entity is IEntityAuditDeletedAt)
            {
                AuditForUpdate(entity);
                AuditForDelete(entity);
                writerContext.Writer.Update(entity);
            }
            else
            {
                writerContext.Writer.Delete(entity);
            }
        }

        private void AuditForCreate(T entity)
        {
            if (entity is IEntityAuditCreated auditCreated)
            {
                auditCreated.CreatedAt = writerContext.DateTimeProvider.UtcNow;
                auditCreated.CreatedBy = writerContext.IdentityProvider.Name;
            }
        }

        private void AuditForUpdate(T entity)
        {
            if (entity is IEntityAuditUpdate auditUpdate)
            {
                auditUpdate.UpdatedAt = writerContext.DateTimeProvider.UtcNow;
                auditUpdate.UpdatedBy = writerContext.IdentityProvider.Name;
            }
        }

        private void AuditForDelete(T entity)
        {
            if (entity is IEntityAuditDeletedAt auditDeleted)
            {
                auditDeleted.DeletedAt = writerContext.DateTimeProvider.UtcNow;
            }
        }
    }
}
