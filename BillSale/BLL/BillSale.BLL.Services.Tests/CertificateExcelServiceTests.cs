using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.BLL.Services.Contracts.Models.Company;
using DocumentFormat.OpenXml.Packaging;

namespace BillSale.BLL.Services.Tests
{
    /// <summary>
    /// Тесты для сервиса генерации Excel-файлов сертификатов
    /// </summary>
    public class CertificateExcelServiceTests
    {
        /// <summary>
        /// Тест для проверки выполнения генерации Excel-файла сертификата
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GenerateCertificateExcelAsync_ShouldGenerateValidExcel()
        {
            // Arrange
            var certificate = CreateCertificateDetailModel();
            var service = new CertificateExcelService();

            // Act
            var stream = await service.GenerateCertificateExcelAsync(
                certificate,
                CancellationToken.None);

            // Assert
            Assert.NotNull(stream);
            Assert.True(stream.Length > 0);

            using (stream)
            using (var document = SpreadsheetDocument.Open(stream, false))
            {
                Assert.NotNull(document.WorkbookPart);
                Assert.NotNull(document.WorkbookPart.Workbook);
            }
        }

        private CertificateDetailModel CreateCertificateDetailModel()
        {
            return new CertificateDetailModel
            {
                Id = Guid.NewGuid(),
                ArticulNumber = 12345,
                City = "Санкт-Петербург",
                Seller = new CompanyModel
                {
                    OrganizationName = "ООО Продавец"
                },
                Purchaser = new CompanyModel
                {
                    OrganizationName = "ООО Покупатель"
                },
                Products =
                [
                    new CertificateProductDetailsModel
                    {
                        ProductName = "Товар 1",
                        MeasureUnit = "шт.",
                        Count = 10,
                        Price = 100
                    }
                ]
            };
        }
    }
}
