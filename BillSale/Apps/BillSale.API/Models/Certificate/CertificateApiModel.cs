namespace BillSale.API.Models.Certificate
{
    /// <summary>
    /// Модель акта приема-передачи
    /// </summary>
    public class CertificateApiModel
    {
        /// <summary>
        /// Идетнификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        public string SellerName { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public string PurchaserName { get; set; } = string.Empty;

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; } = string.Empty;

    }
}
