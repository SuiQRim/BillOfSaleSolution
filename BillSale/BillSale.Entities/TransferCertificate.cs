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
        /// Город составления документа
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Дата оформления документа
        /// </summary>
        public DateTimeOffset PreparationDate { get; set; }

    }
}
