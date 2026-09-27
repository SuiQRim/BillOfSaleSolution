using BillSale.API.Models.Certificate.ProductItem;
using BillSale.API.Models.Company;

namespace BillSale.API.Models.Certificate
{
    /// <summary>
    /// Модель детальной информации об акте
    /// </summary>
    public class CertificateDetailsApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Продавец
        /// </summary>
        public CompanyApiModel Seller { get; set; } = null!;

        /// <summary>
        /// Покупатель
        /// </summary>
        public CompanyApiModel Purchaser { get; set; } = null!;

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
        public IReadOnlyCollection<CertificateProductDetailsApiModel> Products { get; set; } = [];
    }
}
