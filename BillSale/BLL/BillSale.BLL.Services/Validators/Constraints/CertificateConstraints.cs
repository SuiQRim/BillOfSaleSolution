namespace BillSale.BLL.Services.Validators.Constraints
{
    /// <summary>
    /// Ограничения для сертификата
    /// </summary>
    public class CertificateConstraints
    {
        /// <summary>
        /// Максимальная длина наименования города
        /// </summary>
        public const int CityNameMaxLength = 100;

        /// <summary>
        /// Минимальное количество продуктов в позиции
        /// </summary>
        public const int MinCount = 1;

        /// <summary>
        /// Максимальное количество продуктов в позиции
        /// </summary>
        public const int MaxCount = 100000;
    }
}
