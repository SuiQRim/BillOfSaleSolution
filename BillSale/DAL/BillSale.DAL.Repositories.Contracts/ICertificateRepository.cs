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

        /// <summary>
        /// Проверка на наличие активных сертификатов с определенной компанией
        /// </summary>
        /// <param name="companyId">Идентификатор компании</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Флаг есть ли активный сертификат с данной компанией</returns>
        Task<bool> HasActiveCertificatesForCompanyAsync(Guid companyId,
            CancellationToken cancellationToken);

        /// <summary>
        /// Проверка на наличие активных актов с определенным продуктом
        /// </summary>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Флаг есть ли активный сертификат с данным продуктом</returns>
        Task<bool> HasActiveCertificatesForProductAsync(Guid productId,
            CancellationToken cancellationToken);

    }
}
