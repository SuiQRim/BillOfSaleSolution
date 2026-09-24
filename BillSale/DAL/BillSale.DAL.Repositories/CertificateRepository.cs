using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
    /// <summary>
    /// Репозиторий работы с сущностю <see cref="Certificate">
    /// </summary>
    public class CertificateRepository : BaseWriteRepository<Certificate>, ICertificateRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="reader"></param>
        public CertificateRepository(IDbWriterContext writerContext, IReader reader) : base(writerContext)
        {
            this.reader = reader;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Certificate>> GetCertificatesAsync(CancellationToken cancellationToken)
            => await reader.Read<Certificate>()
                .NotDeletedAt()
                .Include(x => x.Purchaser)
                .Include(x => x.Seller)
                .OrderBy(x => x.City)
                .ToReadOnlyCollectionAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<Certificate?> GetCertificateById(Guid id, CancellationToken cancellationToken)
            => await reader.Read<Certificate>()
                .NotDeletedAt()
                .Include(x => x.Purchaser)
                .Include(x => x.Seller)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<Certificate?> GetCertificateDetailById(Guid id, CancellationToken cancellationToken)
           => await reader.Read<Certificate>()
                .NotDeletedAt()
                .Include(x => x.Seller)
                .Include(x => x.Purchaser)
                .Include(x => x.ProductItems.Where(p => p.DeletedAt == null))
                    .ThenInclude(x => x.Product)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
