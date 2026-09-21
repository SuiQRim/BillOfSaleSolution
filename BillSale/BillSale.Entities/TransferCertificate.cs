using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Акт приема-передачи
    /// </summary>
    public class TransferCertificate : BaseAuditEntity
    {
        public int ArticulNumber { get; set; }
        /// <summary>
        /// Список продуктов
        /// </summary>
        public ICollection<TransferCertificateProduct> ProductItems { get; } = [];

        /// <summary>
        /// Идентификатор продовца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary> 
        /// Продовец
        /// </summary>
        public Company Seller { get; set; } = null!;

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid PurchaserId { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        public Company Purchaser { get; set; } = null!;

        /// <summary>
        /// Город составления документа
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Дата оформления документа
        /// </summary>
        public DateTimeOffset PreparationDate { get; set; }

    }
}
