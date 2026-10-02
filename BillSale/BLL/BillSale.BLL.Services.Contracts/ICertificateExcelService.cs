using BillSale.BLL.Services.Contracts.Models.Certificate;

namespace BillSale.BLL.Services.Contracts
{
    /// <summary>
    /// Сервис для генерации сертификатов в формате Excel
    /// </summary>
    public interface ICertificateExcelService
    {
        /// <summary>
        /// Генерация сертификата в формате Excel
        /// </summary>
        /// <param name="certificate">Модель сертификата</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Поток с данными Excel</returns>
        Task<Stream> GenerateCertificateExcelAsync(CertificateDetailModel certificate, CancellationToken cancellationToken);
    }
}
