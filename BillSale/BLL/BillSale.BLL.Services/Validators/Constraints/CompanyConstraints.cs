namespace BillSale.BLL.Services.Validators.Constraints
{
    /// <summary>
    /// Класс, содержащий константы для ограничений компании
    /// </summary>
    public class CompanyConstraints
    {
        /// <summary>
        /// Максимальная длина наименования долждности
        /// </summary>
        public const int PostMaxLength = 100;

        /// <summary>
        /// Максимальная длина имени
        /// </summary>
        public const int FirstNameMaxLength = 20;

        /// <summary>
        /// Максимальная длина фамилии
        /// </summary>
        public const int LastNameMaxLength = 20;

        /// <summary>
        /// Максимальная длина отчества
        /// </summary>
        public const int MiddleNameMaxLength = 20;

        /// <summary>
        /// Максимальная длина наименования документа
        /// </summary>
        public const int DocumentNameMaxLength = 100;

        /// <summary>
        /// Максимальная длина наименования организации
        /// </summary>
        public const int OrganizationNameMaxLength = 100;
    }
}
