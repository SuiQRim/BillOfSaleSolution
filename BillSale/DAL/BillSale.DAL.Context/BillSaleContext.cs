using BillSale.Context;
using BillSale.DAL.Contracts.Repositories;
using BillSale.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Context
{
    /// <summary>
    /// Класс контекста БД
    /// </summary>
    public class BillSaleContext : DbContext, IWriter, IReader, IUnitOfWork
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BillSaleContext"/>
        /// </summary>
        public BillSaleContext(DbContextOptions<BillSaleContext> options)
            : base(options)
        {
            // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntitiesAnchor).Assembly);
            SeedData.Seed(modelBuilder);
        }

        IQueryable<TEntity> IReader.Read<TEntity>()
            => base.Set<TEntity>()
            .AsNoTracking();

        void IWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return count;
        }
    }
}
