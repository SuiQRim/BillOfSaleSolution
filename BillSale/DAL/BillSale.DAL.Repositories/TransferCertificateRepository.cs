using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
    public class TransferCertificateRepository : ITransferCertificateRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="reader"></param>
        public TransferCertificateRepository(IReader reader)
        {
            this.reader = reader;
        }

        public async Task<IReadOnlyCollection<TransferCertificate>> GetSertificateAsync(CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .OrderBy(x => x.City)
                .ToReadOnlyCollectionAsync(cancellationToken);

        public async Task<TransferCertificate?> GetSertificateById(Guid id, CancellationToken cancellationToken)
            => await reader.Read<TransferCertificate>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
