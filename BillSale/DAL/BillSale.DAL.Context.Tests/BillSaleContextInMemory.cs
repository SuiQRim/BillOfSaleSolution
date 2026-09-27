using BillSale.DAL.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BillSale.DAL.Context.Tests
{
    /// <summary>
    /// Контекст <see cref="BillSaleContext"/> для тестов с использованием InMemory базы данных
    /// </summary>
    public class BillSaleContextInMemory : IAsyncDisposable
    {

        /// <summary>
        /// Котекст <see cref="BillSaleContext"/>
        /// </summary>
        protected BillSaleContext Context { get; }


        /// <inheritdoc cref="IUnitOfWork"/>
        protected IUnitOfWork UnitOfWork => Context;


        /// <inheritdoc cref="IDbWriterContext"/>
        protected IDbWriterContext WriterContext => new TestWriterContext(Context);


        /// <summary>
        /// ctor
        /// </summary>
        protected BillSaleContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BillSaleContext>()
                .UseInMemoryDatabase($"BillSaleContextTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            Context = new BillSaleContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IAsyncDisposable.DisposeAsync"/>
        public async ValueTask DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }
    }
}
