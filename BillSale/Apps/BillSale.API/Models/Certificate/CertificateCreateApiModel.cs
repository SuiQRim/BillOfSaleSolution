using BillSale.API.Models.Certificate.ProductItem;

namespace BillSale.API.Models.Certificate
{
    /// <summary>
    /// Модель создания акта
    /// </summary>
    public class CertificateCreateApiModel
    {
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
        public ICollection<CertificateProductCreateApiModel> Products { get; set; } = [];
    }
}
