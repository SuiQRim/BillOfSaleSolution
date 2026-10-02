namespace BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem
{
    /// <summary>
    /// Модель создания продукта в акте
    /// </summary>
    public class CertificateProductCreateModel
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
