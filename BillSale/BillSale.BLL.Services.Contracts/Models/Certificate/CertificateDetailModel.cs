using BillSale.BLL.Services.Contracts.Models.Certificate.Product;
using BillSale.BLL.Services.Contracts.Models.Company;

namespace BillSale.BLL.Services.Contracts.Models.Certificate
{
    /// <summary>
    /// Модель детальной информации об акте
    /// </summary>
    public class CertificateDetailModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Продавец
        /// </summary>
        public CompanyModel Seller { get; set; } = null!;

        /// <summary>
        /// Покупатель
        /// </summary>
        public CompanyModel Purchaser { get; set; } = null!;

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset PreparationDate { get; set; }

        /// <summary>
        /// Продукты в акте
        /// </summary>
        public IReadOnlyCollection<CertificateProductDetailsModel> Products { get; set; } = [];
    }
}
