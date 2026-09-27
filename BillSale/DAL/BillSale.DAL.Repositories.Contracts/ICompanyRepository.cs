using BillSale.DAL.Contracts.Repositories;
using BillSale.Entities;

namespace BillSale.DAL.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий компаний <see cref="Company"/>
    /// </summary>
    public interface ICompanyRepository : IBaseWriteRepository<Company>
    {
        /// <summary>
        /// Получение списка компаний
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<IReadOnlyCollection<Company>> GetCompaniesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение компании по индетификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<Company?> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
