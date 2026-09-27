using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
    /// <summary>
    /// Репозиторий работы с сущностю <see cref="Product">
    /// </summary>
    public class ProductRepository : BaseWriteRepository<Product>, IProductRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="reader"></param>
        public ProductRepository(IDbWriterContext writerContext, IReader reader) : base(writerContext)
        {
            this.reader = reader;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Product>> GetProductsAsync(CancellationToken cancellationToken)
            => await reader.Read<Product>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
            => await reader.Read<Product>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

    }
}
