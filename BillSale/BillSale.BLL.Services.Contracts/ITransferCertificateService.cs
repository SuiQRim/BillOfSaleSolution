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
        Task<IReadOnlyCollection<CertificateModel>> GetCertificates(CancellationToken cancellationToken);

        /// <summary>
        /// Получение акта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Акт</returns>
        Task<CertificateModel> GetCertificateById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение подробного акта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Детальный акт</returns>
        Task<CertificateDetailModel> GetDetailCertificateById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление акта
        /// </summary>
        /// <param name="certificateModel">Акт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task AddCertificate(CertificateCreateModel certificateModel, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление акта
        /// </summary>
        /// <param name="certificateModel">Акт</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task UpdateCertificate(CertificateUpdateModel certificateModel, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление акта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task DeleteCertificate(Guid id, CancellationToken cancellationToken);

    }
}
