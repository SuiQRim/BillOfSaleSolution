using BillSale.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Context
{
    /// <summary>
    /// Класс контекста БД
    /// </summary>
    public class BillSaleContext : DbContext
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
        }
    }
}
