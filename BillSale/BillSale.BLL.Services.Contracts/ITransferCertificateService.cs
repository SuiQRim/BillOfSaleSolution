using BillSale.BLL.Services.Contracts.Models.Certificate;

namespace BillSale.BLL.Services.Contracts
{
    /// <summary>
    /// Контракт сервиса сущности акта
    /// </summary>
    public interface ITransferCertificateService
    {
        /// <summary>
        /// Получение списка актов
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Список актов</returns>
        Task<IReadOnlyCollection<CertificateModel>> GetCertificatesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение акта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Акт</returns>
        Task<CertificateModel> GetCertificateByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение подробного акта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Детальный акт</returns>
        Task<CertificateDetailModel> GetDetailCertificateByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление акта
        /// </summary>
        /// <param name="certificateModel">Акт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<CertificateDetailModel> AddCertificateAsync(CertificateCreateModel certificateModel, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление акта
        /// </summary>
        /// <param name="certificateModel">Акт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task UpdateCertificateAsync(CertificateUpdateModel certificateModel, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление акта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task DeleteCertificateAsync(Guid id, CancellationToken cancellationToken);

    }
}
