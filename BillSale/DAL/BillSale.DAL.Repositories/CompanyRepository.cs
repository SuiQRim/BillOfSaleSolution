using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories
{
    /// <summary>
    /// Репозиторий работы с сущностю <see cref="Company">
    /// </summary>
    public class CompanyRepository : BaseWriteRepository<Company>, ICompanyRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="reader"></param>
        public CompanyRepository(IDbWriterContext writerContext, IReader reader) : base(writerContext)
        {
            this.reader = reader;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
            => await reader.Read<Company>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        /// <inheritdoc />
        public async Task<Company?> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken)
            => await reader.Read<Company>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

    }
}
