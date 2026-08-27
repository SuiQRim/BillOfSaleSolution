using BillSale.Entities;

namespace BillSale.DAL.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий акта-приема передачи <see cref="TransferCertificate"/>
    /// </summary>
    public interface ITransferCertificateRepository
    {
        /// <summary>
        /// Получение списка актов приема-передачи
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<IReadOnlyCollection<TransferCertificate>> GetSertificateAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение акта приема-передачи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task<TransferCertificate?> GetSertificateById(Guid id, CancellationToken cancellationToken);
    }
}
