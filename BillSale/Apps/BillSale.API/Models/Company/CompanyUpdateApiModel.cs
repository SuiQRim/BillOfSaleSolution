namespace BillSale.API.Models.Company
{
    /// <summary>
    /// Данные для обновления компании.
    /// </summary>
    public class CompanyUpdateApiModel
    {
        /// <summary>
        /// Наименование организации
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
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Наименование документа
        /// </summary>
        public string DocumentName { get; set; } = string.Empty;
    }
}
