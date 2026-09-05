namespace BillSale.BLL.Services.Contracts.Models.Company
{
    /// <summary>
    /// Модель создания компании
    /// </summary>
    public class CompanyCreateModel
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
