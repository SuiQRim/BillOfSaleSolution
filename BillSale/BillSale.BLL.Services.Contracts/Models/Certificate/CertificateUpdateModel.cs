using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;

namespace BillSale.BLL.Services.Contracts.Models.Certificate
{
    /// <summary>
    /// Модель обновления акта
    /// </summary>
    public class CertificateUpdateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid PurchaserId { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Продукты в акте
        /// </summary>
        public IReadOnlyCollection<CertificateProductUpdateModel> Products { get; set; } = [];
    }
}
