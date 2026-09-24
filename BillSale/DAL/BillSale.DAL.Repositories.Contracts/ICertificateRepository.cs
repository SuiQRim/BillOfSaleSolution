using BillSale.DAL.Contracts.Repositories;
using BillSale.Entities;

namespace BillSale.DAL.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий акта-приема передачи <see cref="Certificate"/>
    /// </summary>
    public interface ICertificateRepository : IBaseWriteRepository<Certificate>
    {
        /// <summary>
        /// Получение списка актов приема-передачи
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<IReadOnlyCollection<Certificate>> GetCertificatesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение акта приема-передачи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<Certificate?> GetCertificateById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение детального акта приема-передачи с дочерними сущностями
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<Certificate?> GetCertificateDetailById(Guid id, CancellationToken cancellationToken);

    }
}
