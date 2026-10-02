using BillSale.Entities;

namespace BillSale.API.Tests.Infrastructure.TestData
{
    /// <summary>
    /// Модель для тестов с доступом к сгенерированным экземплярам
    /// </summary>
    public class CertificateTestData
    {
        /// <summary>
        /// Сертификат
        /// </summary>
        public Certificate Certificate { get; init; } = null!;

        /// <summary>
        /// Позиция продукта 1
        /// </summary>
        public CertificateProduct ProductItem { get; init; } = null!;

        /// <summary>
        /// Позиция продукта 2
        /// </summary>
        public CertificateProduct ProductItem2 { get; init; } = null!;

        /// <summary>
        /// Покупатель 1
        /// </summary>
        public Company Seller { get; init; } = null!;

        /// <summary>
        /// Покупатель 2
        /// </summary>
        public Company Seller2 { get; init; } = null!;

        /// <summary>
        /// Продавец 1
        /// </summary>
        public Company Purchaser { get; init; } = null!;

        /// <summary>
        /// Продавец 2
        /// </summary>
        public Company Purchaser2 { get; init; } = null!;

        /// <summary>
        /// Продукт 1
        /// </summary>
        public Product Product { get; init; } = null!;

        /// <summary>
        /// Продукт 2
        /// </summary>
        public Product Product2 { get; init; } = null!;
    }
}
