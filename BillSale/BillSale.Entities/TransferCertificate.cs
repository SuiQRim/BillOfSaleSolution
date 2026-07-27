using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Акт приема-передачи
    /// </summary>
    public class TransferCertificate : BaseAuditEntity
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TransferCertificate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Product> Products { get; set; }

        /// <summary>
        /// Идентификатор продовца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Продовец
        /// </summary>
        public Seller Seller { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid PurchaserId { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        public Purchaser Purchaser { get; set; }

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
