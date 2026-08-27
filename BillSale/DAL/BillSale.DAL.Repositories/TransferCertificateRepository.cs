using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

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
            => throw new NotImplementedException();
        public async Task<TransferCertificate> GetSertificateById(Guid id, CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }
}
