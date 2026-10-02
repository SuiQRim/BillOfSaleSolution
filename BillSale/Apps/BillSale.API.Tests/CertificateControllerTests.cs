using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BillSale.API.Models.Certificate;
using BillSale.API.Models.Certificate.ProductItem;
using BillSale.API.Tests.Infrastructure;
using BillSale.API.Tests.Infrastructure.TestData;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.API.Tests
{
    /// <summary>
    /// Интеграционные тесты для контроллера сертификатов
    /// </summary>
    [Collection(nameof(BillsalseApiTestCollection))]
    public class CertificateControllerTests : IntegrationTestBase
    {
        private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fixture">Фикстура для интеграционных тестов</param>
        public CertificateControllerTests(BillSaleApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Проверяет эндпоинт, должен вернуть список сертификатов
        /// </summary>
        [Fact]
        public async Task Get_ReturnsAllCertificates()
        {
            var expected = await SeedCertificateTestDataAsync();
            await SeedCertificateTestDataAsync();

            var client = CreateClient();
            var response = await client.GetAsync("/api/certificate");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var certificates = await response.Content
                .ReadFromJsonAsync<List<CertificateApiModel>>(jsonOptions);

            Assert.NotNull(certificates);
            Assert.Contains(
                certificates,
                x => x.Id == expected.Certificate.Id &&
                     x.ArticulNumber == expected.Certificate.ArticulNumber);
        }

        /// <summary>
        /// Провярет эндпоинт сертификат по id
        /// </summary>
        [Fact]
        public async Task GetById_ReturnCertificate()
        {
            var data = await SeedCertificateTestDataAsync();

            var client = CreateClient();
            var response = await client.GetAsync($"/api/certificate/{data.Certificate.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var certificate = await response.Content
                .ReadFromJsonAsync<CertificateApiModel>(CancellationToken.None);

            Assert.NotNull(certificate);
            Assert.Equal(data.Certificate.Id, certificate.Id);
            Assert.Equal(data.Seller.OrganizationName, certificate.SellerName);
            Assert.Equal(data.Purchaser.OrganizationName, certificate.PurchaserName);
            Assert.Equal(data.Certificate.ArticulNumber, certificate.ArticulNumber);
        }

        /// <summary>
        /// Проверяет эндпоинт сертификат не найден с id
        /// </summary>
        [Fact]
        public async Task GetById_WhenNotFound_Returns404()
        {
            await SeedCertificateTestDataAsync();

            var client = CreateClient();
            var response = await client.GetAsync($"/api/certificate/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест получить детальный сертификат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetDetailsById_ReturnCertificate()
        {
            var data = await SeedCertificateTestDataAsync();

            var client = CreateClient();
            var response = await client.GetAsync(
                $"/api/certificate/details/{data.Certificate.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var certificate = await response.Content
                .ReadFromJsonAsync<CertificateDetailsApiModel>();

            Assert.NotNull(certificate);
            Assert.Equal(data.Certificate.Id, certificate.Id);
            Assert.Equal(data.Certificate.City, certificate.City);
            Assert.Equal(data.Certificate.ArticulNumber, certificate.ArticulNumber);
            Assert.Equal(data.Seller.Id, certificate.Seller.Id);
            Assert.Equal(data.Purchaser.Id, certificate.Purchaser.Id);
            Assert.Equal(data.Certificate.ProductItems.Count, certificate.Products.Count);
        }

        /// <summary>
        /// Тест получить сертификат которого нет
        /// </summary>
        [Fact]
        public async Task GetByIdDetails_WhenNotFound_Returns404()
        {
            await SeedCertificateTestDataAsync();

            var client = CreateClient();
            var response = await client.GetAsync(
                $"/api/certificate/details/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест создать сертификат
        /// </summary>
        [Fact]
        public async Task CreateCertificate_CreatesCertificate()
        {
            var data = await SeedCertificateTestDataAsync();

            var firstProduct = new CertificateProductCreateApiModel
            {
                ProductId = data.Product.Id,
                Count = 2,
                Price = 150
            };

            var secondProduct = new CertificateProductCreateApiModel
            {
                ProductId = data.Product2.Id,
                Count = 3,
                Price = 250
            };

            var request = new CertificateCreateApiModel
            {
                SellerId = data.Seller2.Id,
                PurchaserId = data.Purchaser2.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    firstProduct,
                    secondProduct
                ]
            };

            var client = CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content
                .ReadFromJsonAsync<CertificateDetailsApiModel>();

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(request.City, result.City);

            var product = result.Products
                .Single(x => x.ProductId == firstProduct.ProductId);

            Assert.Equal(firstProduct.Count, product.Count);
            Assert.Equal(firstProduct.Price, product.Price);

            var product2 = result.Products
                .Single(x => x.ProductId == secondProduct.ProductId);

            Assert.Equal(secondProduct.Count, product2.Count);
            Assert.Equal(secondProduct.Price, product2.Price);

            var certificate = await Context.Set<Certificate>()
                .Include(x => x.ProductItems)
                .SingleAsync(x => x.Id == result.Id, CancellationToken.None);

            Assert.Equal(request.SellerId, certificate.SellerId);
            Assert.Equal(request.PurchaserId, certificate.PurchaserId);
            Assert.Equal(request.City, certificate.City);
            Assert.Equal(2, certificate.ProductItems.Count);
        }

        /// <summary>
        /// Тест создать сертификат когда такого продукта нет
        /// </summary>
        [Fact]
        public async Task CreateCertificate_WhenProductNotFound_Returns404()
        {
            var seller = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Продавец"
            };

            var purchaser = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Покупатель"
            };

            Context.AddRange(seller, purchaser);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var request = new CertificateCreateApiModel
            {
                SellerId = seller.Id,
                PurchaserId = purchaser.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    new CertificateProductCreateApiModel {
                        ProductId = Guid.NewGuid(),
                        Count = 1,
                        Price = 1
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест создать сертификат когда такого покупателя нет
        /// </summary>
        [Fact]
        public async Task CreateCertificate_WhenSellerNotFound_Returns404()
        {
            var purchaser = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Покупатель"
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Продукт",
                MeasureUnit = "шт."
            };

            Context.AddRange(product, purchaser);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var request = new CertificateCreateApiModel
            {
                SellerId = Guid.NewGuid(),
                PurchaserId = purchaser.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    new CertificateProductCreateApiModel {
                        ProductId = product.Id,
                        Count = 1,
                        Price = 1
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест создать сертификат когда такого продавца нет
        /// </summary>
        [Fact]
        public async Task CreateCertificate_WhenPurchaserNotFound_Returns404()
        {
            var seller = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Продавец"
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Продукт",
                MeasureUnit = "шт."
            };

            Context.AddRange(product, seller);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var request = new CertificateCreateApiModel
            {
                SellerId = seller.Id,
                PurchaserId = Guid.NewGuid(),
                City = "Санкт-Петербург",
                Products =
                [
                    new CertificateProductCreateApiModel {
                        ProductId = product.Id,
                        Count = 1,
                        Price = 1
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест создать сертификат когда модель не валидна
        /// </summary>
        [Fact]
        public async Task CreateCertificate_WhenCityIsEmpty_Returns422()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateCreateApiModel
            {
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = string.Empty,
                Products =
                [
                    new CertificateProductCreateApiModel
                    {
                        ProductId = data.Product.Id,
                        Count = 1,
                        Price = 1
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenValidRequest_UpdatesCertificateAndProductItems()
        {
            var data = await SeedCertificateTestDataAsync();

            var updateProdItem = new CertificateProductUpdateApiModel
            {
                Id = data.ProductItem.Id,
                ProductId = data.Product.Id,
                Count = 999,
                Price = 999
            };

            var newProdItem = new CertificateProductUpdateApiModel
            {
                ProductId = data.Product2.Id,
                Count = 1,
                Price = 1
            };

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    updateProdItem,
                    newProdItem
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var certificate = await Context.Set<Certificate>()
                .SingleAsync(x => x.Id == data.Certificate.Id, CancellationToken.None);

            Assert.Equal(request.City, certificate.City);

            var productItems = await Context.Set<CertificateProduct>()
                .Where(x => x.CertificateId == data.Certificate.Id)
                .ToListAsync(CancellationToken.None);

            var updatedProductItem = productItems
                .Single(x => x.Id == data.ProductItem.Id);

            Assert.Equal(updateProdItem.ProductId, updatedProductItem.ProductId);
            Assert.Equal(updateProdItem.Count, updatedProductItem.Count);
            Assert.Equal(updateProdItem.Price, updatedProductItem.Price);

            var addedProductItem = productItems
                .Single(x => x.ProductId == data.Product2.Id &&
                             x.Id != data.ProductItem2.Id);

            Assert.Equal(newProdItem.Count, addedProductItem.Count);
            Assert.Equal(newProdItem.Price, addedProductItem.Price);
        }

        /// <summary>
        /// Тест обновить сертификат, но такого сертификата нет
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenCertificateNotFound_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = Guid.NewGuid(),
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = "Санкт-Петербург",
                Products = [
                    new CertificateProductUpdateApiModel
                    {
                        ProductId = Guid.NewGuid(),
                        Count = 5,
                        Price = 100
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат но такого продавца нет
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenSellerNotFound_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = Guid.NewGuid(),
                PurchaserId = data.Purchaser.Id,
                City = "Санкт-Петербург",
                Products = [
                    new CertificateProductUpdateApiModel
                    {
                        ProductId = Guid.NewGuid(),
                        Count = 5,
                        Price = 100
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат, но такого покупателя нет
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenPurchaserNotFound_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = Guid.NewGuid(),
                City = "Санкт-Петербург",
                Products = [
                    new CertificateProductUpdateApiModel
                    {
                        ProductId = Guid.NewGuid(),
                        Count = 5,
                        Price = 100
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат, но такого продукта нет
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenProductNotFound_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var newProductItem = new CertificateProductUpdateApiModel
            {
                ProductId = Guid.NewGuid(),
                Count = 5,
                Price = 100
            };

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = data.Certificate.City,
                Products =
                [
                    newProductItem
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат, но позиция продукта не может быть отредактирована т.к. не относиться к редактируемому сертификату
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenProductItemBelongsToAnotherCertificate_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var anotherCertificate = new Certificate
            {
                Id = Guid.NewGuid(),
                SellerId = data.Seller2.Id,
                PurchaserId = data.Purchaser2.Id,
                City = "Казань"
            };

            var anotherProductItem = new CertificateProduct
            {
                Id = Guid.NewGuid(),
                CertificateId = anotherCertificate.Id,
                ProductId = data.Product2.Id,
                Count = 10,
                Price = 200
            };

            Context.AddRange(
                anotherCertificate,
                anotherProductItem);

            await UnitOfWork.SaveChangesAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    new CertificateProductUpdateApiModel
                    {
                        Id = anotherProductItem.Id,
                        ProductId = data.Product2.Id,
                        Count = 999,
                        Price = 999
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат, но такой позиции продукта нет
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenProductItemNotFound_Returns404()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = "Санкт-Петербург",
                Products =
                [
                    new CertificateProductUpdateApiModel
                    {
                        Id = Guid.NewGuid(),
                        ProductId = data.Product.Id,
                        Count = 10,
                        Price = 100
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест обновить сертификат, когда модель не валидна
        /// </summary>
        [Fact]
        public async Task UpdateCertificate_WhenCityIsEmpty_Returns422()
        {
            var data = await SeedCertificateTestDataAsync();

            var request = new CertificateUpdateApiModel
            {
                Id = data.Certificate.Id,
                SellerId = data.Seller.Id,
                PurchaserId = data.Purchaser.Id,
                City = string.Empty,
                Products =
                [
                    new CertificateProductUpdateApiModel
                    {
                        Id = data.ProductItem.Id,
                        ProductId = data.Product.Id,
                        Count = 1,
                        Price = 1
                    }
                ]
            };

            var client = CreateClient();

            var response = await client.PutAsJsonAsync(
                "/api/certificate",
                request);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

        }

        /// <summary>
        /// Тест удаления
        /// </summary>
        [Fact]
        public async Task DeleteCertificate_WhenCertificateExists_ReturnsNoContent()
        {
            var data = await SeedCertificateTestDataAsync();

            var client = CreateClient();

            var response = await client.DeleteAsync(
                $"/api/certificate/{data.Certificate.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Тест удаления когда сертификата нет
        /// </summary>
        [Fact]
        public async Task DeleteCertificate_WhenCertificateNotFound_Returns404()
        {
            var client = CreateClient();

            var response = await client.DeleteAsync(
                $"/api/certificate/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<CertificateTestData> SeedCertificateTestDataAsync()
        {
            var seller = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Продавец 1"
            };

            var seller2 = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Продавец 2"
            };

            var purchaser = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Покупатель 1"
            };

            var purchaser2 = new Company
            {
                Id = Guid.NewGuid(),
                OrganizationName = "ООО Покупатель 2"
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Товар 1"
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Товар 2"
            };

            var certificate = new Certificate
            {
                Id = Guid.NewGuid(),
                SellerId = seller.Id,
                PurchaserId = purchaser.Id,
                City = "Москва"
            };

            var productItem = new CertificateProduct
            {
                Id = Guid.NewGuid(),
                CertificateId = certificate.Id,
                ProductId = product.Id,
                Count = 5,
                Price = 100
            };

            var productItem2 = new CertificateProduct
            {
                Id = Guid.NewGuid(),
                CertificateId = certificate.Id,
                ProductId = product2.Id,
                Count = 10,
                Price = 250
            };

            certificate.ProductItems.Add(productItem);
            certificate.ProductItems.Add(productItem2);

            Context.AddRange(
                seller,
                seller2,
                purchaser,
                purchaser2,
                product,
                product2,
                certificate);

            await UnitOfWork.SaveChangesAsync();

            return new CertificateTestData
            {
                Certificate = certificate,
                ProductItem = productItem,
                ProductItem2 = productItem2,
                Seller = seller,
                Seller2 = seller2,
                Purchaser = purchaser,
                Purchaser2 = purchaser2,
                Product = product,
                Product2 = product2
            };
        }
    }
}
