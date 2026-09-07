namespace BillSale.API.Models
{
    public class CertificateApiModel
    {
        /// <summary>
        /// Идетнификатор
        /// </summary>
        public Guid Id { get; set; }

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
