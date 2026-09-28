using Ahatornn.TestGenerator;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories.Tests
{
    /// <summary>
    /// Класс тестов проверяющих <see cref="CertificateRepository"/>
    /// </summary>
    public class CertificateRepositoryTests : BillSaleContextInMemory
    {
        private readonly ICertificateRepository certificateRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public CertificateRepositoryTests()
        {
            certificateRepository = new CertificateRepository(WriterContext, Context);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificatesAsync"/>,
        /// что при отсутствии актов в базе возвращается пустая коллекция.
        /// </summary>
        [Fact]
        public async Task GetCertificatesShouldReturnEmpty()
        {
            // Arrange

            // Act
            var certificates = await certificateRepository
                .GetCertificatesAsync(CancellationToken.None);

            // Assert
            certificates.Should().NotBeNull().And.BeEmpty();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificatesAsync"/>,
        /// что метод возвращает все существующие акты.
        /// </summary>
        [Fact]
        public async Task GetCertificatesShouldReturnValue()
        {
            // Arrange
            var seller1 = TestEntityProvider.Shared.Create<Company>();
            var purchaser1 = TestEntityProvider.Shared.Create<Company>();
            var certificate1 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller1.Id;
                    x.PurchaserId = purchaser1.Id;
                });

            var seller2 = TestEntityProvider.Shared.Create<Company>();
            var purchaser2 = TestEntityProvider.Shared.Create<Company>();
            var certificate2 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller2.Id;
                    x.PurchaserId = purchaser2.Id;
                });

            var seller3 = TestEntityProvider.Shared.Create<Company>();
            var purchaser3 = TestEntityProvider.Shared.Create<Company>();
            var certificate3 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller3.Id;
                    x.PurchaserId = purchaser3.Id;
                });

            Context.AddRange(
                seller1,
                purchaser1,
                certificate1,
                seller2,
                purchaser2,
                certificate2,
                seller3,
                purchaser3,
                certificate3);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var certificates = await certificateRepository
                .GetCertificatesAsync(CancellationToken.None);

            // Assert
            certificates.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.ContainSingle(x => x.Id == certificate1.Id)
                .And.ContainSingle(x => x.Id == certificate2.Id)
                .And.ContainSingle(x => x.Id == certificate3.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificatesAsync"/>,
        /// что метод не возвращает акты, помеченные как удалённые.
        /// </summary>
        [Fact]
        public async Task GetCertificatesShouldNotReturnDeletedCertificates()
        {
            // Arrange
            var seller1 = TestEntityProvider.Shared.Create<Company>();
            var purchaser1 = TestEntityProvider.Shared.Create<Company>();
            var certificate1 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller1.Id;
                    x.PurchaserId = purchaser1.Id;
                });

            var seller2 = TestEntityProvider.Shared.Create<Company>();
            var purchaser2 = TestEntityProvider.Shared.Create<Company>();
            var certificate2 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller2.Id;
                    x.PurchaserId = purchaser2.Id;
                });

            var deletedSeller = TestEntityProvider.Shared.Create<Company>();
            var deletedPurchaser = TestEntityProvider.Shared.Create<Company>();
            var deletedCertificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = deletedSeller.Id;
                    x.PurchaserId = deletedPurchaser.Id;
                    x.DeletedAt = DateTimeOffset.Now;
                });

            Context.AddRange(
                seller1,
                purchaser1,
                certificate1,
                seller2,
                purchaser2,
                certificate2,
                deletedSeller,
                deletedPurchaser,
                deletedCertificate);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var certificates = await certificateRepository
                .GetCertificatesAsync(CancellationToken.None);

            // Assert
            certificates.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainSingle(x => x.Id == certificate1.Id)
                .And.ContainSingle(x => x.Id == certificate2.Id)
                .And.NotContain(x => x.Id == deletedCertificate.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificatesAsync"/>,
        /// что метод возвращает акты, отсортированные по городу.
        /// </summary>
        [Fact]
        public async Task GetCertificatesShouldReturnOrderedByCity()
        {
            // Arrange
            var seller1 = TestEntityProvider.Shared.Create<Company>();
            var purchaser1 = TestEntityProvider.Shared.Create<Company>();
            var certificate1 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller1.Id;
                    x.PurchaserId = purchaser1.Id;
                    x.City = "C";
                });

            var seller2 = TestEntityProvider.Shared.Create<Company>();
            var purchaser2 = TestEntityProvider.Shared.Create<Company>();
            var certificate2 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller2.Id;
                    x.PurchaserId = purchaser2.Id;
                    x.City = "A";
                });

            var seller3 = TestEntityProvider.Shared.Create<Company>();
            var purchaser3 = TestEntityProvider.Shared.Create<Company>();
            var certificate3 = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller3.Id;
                    x.PurchaserId = purchaser3.Id;
                    x.City = "B";
                });

            Context.AddRange(
                seller1,
                purchaser1,
                certificate1,
                seller2,
                purchaser2,
                certificate2,
                seller3,
                purchaser3,
                certificate3);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var certificates = await certificateRepository
                .GetCertificatesAsync(CancellationToken.None);

            // Assert
            certificates
                .Select(x => x.City)
                .Should()
                .Equal("A", "B", "C");
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificatesAsync"/>,
        /// что метод загружает продавца и покупателя каждого акта.
        /// </summary>
        [Fact]
        public async Task GetCertificatesShouldLoadSellerAndPurchaser()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var certificates = await certificateRepository
                .GetCertificatesAsync(CancellationToken.None);

            // Assert
            var result = certificates.Should().ContainSingle().Subject;

            result.Seller.Should().NotBeNull();
            result.Seller.Id.Should().Be(seller.Id);

            result.Purchaser.Should().NotBeNull();
            result.Purchaser.Id.Should().Be(purchaser.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateById"/>,
        /// что метод возвращает существующий акт по его идентификатору.
        /// </summary>
        [Fact]
        public async Task GetCertificateByIdShouldReturnValue()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(certificate.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateById"/>,
        /// что метод возвращает null, если акт с указанным идентификатором не существует.
        /// </summary>
        [Fact]
        public async Task GetCertificateByIdShouldReturnNullWhenCertificateDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await certificateRepository
                .GetCertificateById(id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateById"/>,
        /// что метод возвращает null для акта, помеченного как удалённый.
        /// </summary>
        [Fact]
        public async Task GetCertificateByIdShouldReturnNullForDeletedCertificate()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                    x.DeletedAt = DateTimeOffset.Now;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateById"/>,
        /// что метод загружает продавца и покупателя акта.
        /// </summary>
        [Fact]
        public async Task GetCertificateByIdShouldLoadSellerAndPurchaser()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.Seller.Should().NotBeNull();
            result.Seller.Id.Should().Be(seller.Id);

            result.Purchaser.Should().NotBeNull();
            result.Purchaser.Id.Should().Be(purchaser.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод возвращает существующий акт.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldReturnValue()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(certificate.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод возвращает null, если акт с указанным идентификатором не существует.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldReturnNullWhenCertificateDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод возвращает null для акта, помеченного как удалённый.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldReturnNullForDeletedCertificate()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                    x.DeletedAt = DateTimeOffset.Now;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод загружает продавца и покупателя акта.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldLoadSellerAndPurchaser()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.Seller.Should().NotBeNull();
            result.Seller.Id.Should().Be(seller.Id);

            result.Purchaser.Should().NotBeNull();
            result.Purchaser.Id.Should().Be(purchaser.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод загружает позиции акта.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldLoadProductItems()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            var certificateProduct1 = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product1.Id;
                });

            var certificateProduct2 = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product2.Id;
                });

            Context.AddRange(
                seller,
                purchaser,
                product1,
                product2,
                certificate,
                certificateProduct1,
                certificateProduct2);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.ProductItems.Should()
                .HaveCount(2)
                .And.ContainSingle(x => x.Id == certificateProduct1.Id)
                .And.ContainSingle(x => x.Id == certificateProduct2.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод загружает продукт для каждой позиции акта.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldLoadProducts()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            var certificateProduct1 = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product1.Id;
                });

            var certificateProduct2 = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product2.Id;
                });

            Context.AddRange(
                seller,
                purchaser,
                product1,
                product2,
                certificate,
                certificateProduct1,
                certificateProduct2);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.ProductItems
                .Should()
                .AllSatisfy(x => x.Product.Should().NotBeNull())
                .And
                .ContainSingle(x => x.Product.Id == product1.Id)
                .And
                .ContainSingle(x => x.Product.Id == product2.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateRepository.GetCertificateDetailById"/>,
        /// что метод не возвращает позиции акта, помеченные как удалённые.
        /// </summary>
        [Fact]
        public async Task GetCertificateDetailByIdShouldNotReturnDeletedProductItems()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            var certificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product1.Id;
                });

            var deletedCertificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product2.Id;
                    x.DeletedAt = DateTimeOffset.Now;
                });

            Context.AddRange(
                seller,
                purchaser,
                product1,
                product2,
                certificate,
                certificateProduct,
                deletedCertificateProduct);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await certificateRepository
                .GetCertificateDetailById(certificate.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.ProductItems
                .Should()
                .ContainSingle(x => x.Id == certificateProduct.Id)
                .And.NotContain(x => x.Id == deletedCertificateProduct.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IBaseWriteRepository{T}.Add"/> у <see cref="ICertificateRepository"/>,
        /// что новый акт успешно добавляется в базу данных.
        /// </summary>
        [Fact]
        public async Task AddShouldCreateCertificate()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            Context.AddRange(seller, purchaser);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            // Act
            certificateRepository.Add(certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await certificateRepository
                .GetCertificateById(certificate.Id, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(certificate.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IBaseWriteRepository{T}.Update"/> у <see cref="ICertificateRepository"/>,
        /// что изменения существующего акта успешно сохраняются в базе данных.
        /// </summary>
        [Fact]
        public async Task UpdateShouldUpdateCertificate()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                    x.ArticulNumber = 100;
                    x.City = "Москва";
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            certificate.ArticulNumber = 200;
            certificate.City = "Санкт-Петербург";

            // Act
            certificateRepository.Update(certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await certificateRepository
                .GetCertificateById(certificate.Id, CancellationToken.None);

            result.Should().NotBeNull();
            result!.ArticulNumber.Should().Be(200);
            result.City.Should().Be("Санкт-Петербург");
        }

        /// <summary>
        /// Проверяет метод <see cref="IBaseWriteRepository{T}.Delete"/> у <see cref="ICertificateRepository"/>,
        /// что после удаления акт помечается как удалённый.
        /// </summary>
        [Fact]
        public async Task DeleteShouldMarkCertificateAsDeleted()
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                });

            Context.AddRange(seller, purchaser, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            certificateRepository.Delete(certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await Context.Set<Certificate>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == certificate.Id);

            result.Should().NotBeNull();
            result!.DeletedAt.Should().NotBeNull();
        }
    }
}
