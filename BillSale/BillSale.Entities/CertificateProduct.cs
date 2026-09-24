using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Продукт в акте
    /// </summary>
    public class CertificateProduct : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор акта
        /// </summary>
        public Guid CertificateId { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Количество продуктов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Цена за штуку
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Итоговая цена
        /// </summary>
        public decimal TotalPrice => Count * Price;
    }
}
