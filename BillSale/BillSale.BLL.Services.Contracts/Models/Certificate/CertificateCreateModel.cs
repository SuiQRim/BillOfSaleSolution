using BillSale.BLL.Services.Contracts.Models.Certificate.Product;

namespace BillSale.BLL.Services.Contracts.Models.Certificate
{
    /// <summary>
    /// Модель создания акта
    /// </summary>
    public class CertificateCreateModel
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
        public ICollection<CertificateProductCreateModel> Products { get; set; } = [];
    }
}
