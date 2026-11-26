namespace BillSale.Entities.Constraints
{
    /// <summary>
    /// Ограничение для позиций сертификата
    /// </summary>
    public static class CertificateProductConstraints
    {
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
