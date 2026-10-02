namespace BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem
{
    /// <summary>
    /// Подробная модель продукта сертификата
    /// </summary>
    public class CertificateProductDetailsModel
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
        /// Наименование продукта
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Еденица измерения
        /// </summary>
        public string MeasureUnit { get; set; } = string.Empty;

        /// <summary>
        /// Количество
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
