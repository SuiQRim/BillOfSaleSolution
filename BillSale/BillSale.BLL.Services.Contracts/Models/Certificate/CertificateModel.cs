namespace BillSale.BLL.Services.Contracts.Models.Certificate
{
    /// <summary>
    /// Модель акта приема-передачи
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// Идетнификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public int ArticulNumber { get; set; }

        /// <summary>
        /// Имя продавца
        /// </summary>
        public string SellerName { get; set; } = string.Empty;

        /// <summary>
        /// Имя покупателя
        /// </summary>
        public string PurchaserName { get; set; } = string.Empty;

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset PreparationDate { get; set; }
    }
}
