using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Компания
    /// </summary>
    public abstract class Company : BaseAuditEntity
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string OrganizationName { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public string Post { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилие
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Название подтверждающего документа
        /// </summary>
        public string DocumentName { get; set; } = string.Empty;
    }
}
