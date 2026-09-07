using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
    /// <summary>
    /// Репозиторий работы с сущностю <see cref="TransferCertificate">
    /// </summary>
    public class TransferCertificateRepository : BaseWriteRepository<TransferCertificate>, ITransferCertificateRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="reader"></param>
        public TransferCertificateRepository(IDbWriterContext writerContext, IReader reader) : base(writerContext)
        {
            this.reader = reader;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<TransferCertificate>> GetCertificatesAsync(CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .Include(x => x.Purchaser)
                .Include(x => x.Seller)
                .OrderBy(x => x.City)
                .ToReadOnlyCollectionAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<TransferCertificate?> GetCertificateById(Guid id, CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .Include(x => x.Purchaser)
                .Include(x => x.Seller)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<TransferCertificate?> GetCertificateDetailById(Guid id, CancellationToken cancellationToken)
           => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .Include(x => x.Seller)
                .Include(x => x.Purchaser)
                .Include(x => x.ProductItems)
                    .ThenInclude(x => x.Product)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
