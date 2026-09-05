namespace BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem
{
    /// <summary>
    /// Модель обновления продукта в акте
    /// </summary>
    public class CertificateProductUpdateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
