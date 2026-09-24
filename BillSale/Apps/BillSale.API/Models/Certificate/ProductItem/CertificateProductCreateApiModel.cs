namespace BillSale.API.Models.Certificate.ProductItem
{
    /// <summary>
    /// Модель создания продукта в акте
    /// </summary>
    public class CertificateProductCreateApiModel
    {
        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Цена за штуку
        /// </summary>
        public decimal Price { get; set; }
    }
}
