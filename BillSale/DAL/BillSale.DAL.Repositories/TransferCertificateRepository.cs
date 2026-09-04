using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
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

        public async Task<IReadOnlyCollection<TransferCertificate>> GetCertificatesAsync(CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .OrderBy(x => x.City)
                .ToReadOnlyCollectionAsync(cancellationToken);

        public async Task<TransferCertificate?> GetCertificateById(Guid id, CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
