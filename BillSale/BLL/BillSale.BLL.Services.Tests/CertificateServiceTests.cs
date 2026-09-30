using Ahatornn.TestGenerator;
using AutoMapper;
using BillSale.BLL.Services.Automapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories;
using BillSale.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BillSale.BLL.Services.Tests;

/// <summary>
/// Тесты для <see cref="CertificateService"/>
/// </summary>
public class CertificateServiceTests : BillSaleContextInMemory
{
    private readonly ICertificateService certificateService;

    /// <summary>
    /// Инициализирует новый экземпляр тестов сервиса сертификатов
    /// </summary>
    public CertificateServiceTests()
    {
        var certificateRepository = new CertificateRepository(WriterContext, Context);
        var productItemRepository = new CertificateProductItemRepository(WriterContext);
        var productRepository = new ProductRepository(WriterContext, Context);
        var companyRepository = new CompanyRepository(WriterContext, Context);

        var profile = new ServiceProfile();
        var mapper = new MapperConfiguration(
            x => x.AddProfile(profile),
            NullLoggerFactory.Instance)
            .CreateMapper();

        certificateService = new CertificateService(
            certificateRepository,
            productItemRepository,
            productRepository,
            companyRepository,
            Context,
            mapper);
    }

    /// <summary>
    /// Проверяет получение сертификатов из пустой базы данных
    /// </summary>
    [Fact]
    public async Task GetCertificatesShouldReturnEmpty()
    {
        // Act
        var items = await certificateService.GetCertificatesAsync(CancellationToken.None);

        // Assert
        items.Should().NotBeNull().And.BeEmpty();
    }

    /// <summary>
    /// Проверяет получение сертификатов из базы данных
    /// </summary>
    [Fact]
    public async Task GetCertificatesShouldReturnValue()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();

        var item1 = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.City = "Москва";
            });

        var item2 = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.City = "Санкт-Петербург";
            });

        Context.AddRange(seller, purchaser, item1, item2);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var items = await certificateService.GetCertificatesAsync(CancellationToken.None);

        // Assert
        items.Should()
            .NotBeNull()
            .And.HaveCount(2)
            .And.ContainSingle(x => x.Id == item1.Id)
            .And.ContainSingle(x => x.Id == item2.Id);
    }

    /// <summary>
    /// Проверяет получение сертификата по идентификатору
    /// </summary>
    [Fact]
    public async Task GetCertificateByIdShouldReturnValue()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();

        var entity = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
            });

        Context.AddRange(seller, purchaser, entity);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await certificateService.GetCertificateByIdAsync(
            entity.Id,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
    }

    /// <summary>
    /// Проверяет получение сертификата по идентификатору, когда сертификат не существует
    /// </summary>
    [Fact]
    public async Task GetCertificateByIdShouldThrowNotFoundException()
    {
        // Act
        var act = () => certificateService.GetCertificateByIdAsync(
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Certificate>>();
    }

    /// <summary>
    /// Проверяет получение детальной информации о сертификате
    /// </summary>
    [Fact]
    public async Task GetDetailCertificateByIdShouldReturnValue()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

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
                x.ProductId = product.Id;
            });

        certificate.ProductItems.Add(certificateProduct);

        Context.AddRange(
            seller,
            purchaser,
            product,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await certificateService.GetDetailCertificateByIdAsync(
            certificate.Id,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(certificate.Id);
    }

    /// <summary>
    /// Проверяет получение детальной информации о сертификате, когда сертификат не существует
    /// </summary>
    [Fact]
    public async Task GetDetailCertificateByIdShouldThrowNotFoundException()
    {
        // Act
        var act = () => certificateService.GetDetailCertificateByIdAsync(
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Certificate>>();
    }

    /// <summary>
    /// Проверяет добавление сертификата
    /// </summary>
    [Fact]
    public async Task AddCertificateShouldWork()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

        Context.AddRange(seller, purchaser, product);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var createModel = TestEntityProvider.Shared.Create<CertificateCreateModel>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductCreateModel>(
                        p => p.ProductId = product.Id)
                ];
            });

        // Act
        var result = await certificateService.AddCertificateAsync(
            createModel,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Seller.Id.Should().Be(seller.Id);
        result.Purchaser.Id.Should().Be(purchaser.Id);

        Context.Set<Certificate>()
            .Should()
            .ContainSingle(x => x.Id == result.Id);

        Context.Set<CertificateProduct>()
            .Should()
            .ContainSingle(x =>
                x.CertificateId == result.Id &&
                x.ProductId == product.Id);
    }

    /// <summary>
    /// Проверяет добавление сертификата с несуществующим продавцом
    /// </summary>
    [Fact]
    public async Task AddCertificateShouldThrowNotFoundExceptionWhenSellerDoesNotExist()
    {
        // Arrange
        var purchaser = TestEntityProvider.Shared.Create<Company>();

        Context.Add(purchaser);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var createModel = TestEntityProvider.Shared.Create<CertificateCreateModel>(
            x =>
            {
                x.SellerId = Guid.NewGuid();
                x.PurchaserId = purchaser.Id;
            });

        // Act
        var act = () => certificateService.AddCertificateAsync(
            createModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
    }

    /// <summary>
    /// Проверяет добавление сертификата с несуществующим покупателем
    /// </summary>
    [Fact]
    public async Task AddCertificateShouldThrowNotFoundExceptionWhenPurchaserDoesNotExist()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();

        Context.Add(seller);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var createModel = TestEntityProvider.Shared.Create<CertificateCreateModel>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = Guid.NewGuid();
            });

        // Act
        var act = () => certificateService.AddCertificateAsync(
            createModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
    }

    /// <summary>
    /// Проверяет добавление сертификата с несуществующим продуктом
    /// </summary>
    [Fact]
    public async Task AddCertificateShouldThrowNotFoundExceptionWhenProductDoesNotExist()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();

        Context.AddRange(seller, purchaser);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var createModel = TestEntityProvider.Shared.Create<CertificateCreateModel>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductCreateModel>(
                        p => p.ProductId = Guid.NewGuid())
                ];
            });

        // Act
        var act = () => certificateService.AddCertificateAsync(
            createModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
    }

    /// <summary>
    /// Проверяет обновление сертификата
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldWork()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

        var certificate = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.City = "Москва";
            });

        var certificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>(
            x =>
            {
                x.CertificateId = certificate.Id;
                x.ProductId = product.Id;
                x.Count = 1;
            });

        certificate.ProductItems.Add(certificateProduct);

        Context.AddRange(
            seller,
            purchaser,
            product,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                        p =>
                        {
                            p.Id = certificateProduct.Id;
                            p.ProductId = product.Id;
                            p.Count = 5;
                        })
                ];
            });

        // Act
        await certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        var result = await Context.Set<CertificateProduct>()
            .FindAsync(certificateProduct.Id);

        result.Should().NotBeNull();
        result!.Count.Should().Be(updateModel.Products.First().Count);
    }

    /// <summary>
    /// Проверяет обновление несуществующего сертификата
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldThrowNotFoundException()
    {
        // Arrange
        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x => x.Id = Guid.NewGuid());

        // Act
        var act = () => certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Certificate>>();
    }

    /// <summary>
    /// Проверяет обновление сертификата с несуществующей компанией
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldThrowNotFoundExceptionWhenCompanyDoesNotExist()
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

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = Guid.NewGuid();
                x.PurchaserId = purchaser.Id;
            });

        // Act
        var act = () => certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
    }

    /// <summary>
    /// Проверяет обновление сертификата с несуществующей позиции продукта
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldThrowNotFoundExceptionWhenProductItemDoesNotExist()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

        var certificate = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
            });

        Context.AddRange(
            seller,
            purchaser,
            product,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                        p =>
                        {
                            p.Id = Guid.NewGuid();
                            p.ProductId = product.Id;
                        })
                ];
            });

        // Act
        var act = () => certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    /// <summary>
    /// Проверяет обновление сертификата с несуществующим продуктом
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldThrowNotFoundExceptionWhenProductDoesNotExist()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

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
                x.ProductId = product.Id;
            });

        certificate.ProductItems.Add(certificateProduct);

        Context.AddRange(
            seller,
            purchaser,
            product,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                    p =>
                    {
                        p.Id = certificateProduct.Id;
                        p.ProductId = Guid.NewGuid();
                    })
                ];
            });

        // Act
        var act = () => certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
    }

    /// <summary>
    /// Проверяет добавление новой позиции продукта при обновлении сертификата
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldAddNewProductItem()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();
        var product = TestEntityProvider.Shared.Create<Product>();

        var certificate = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
            });

        Context.AddRange(
            seller,
            purchaser,
            product,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                    p =>
                    {
                        p.Id = Guid.Empty;
                        p.ProductId = product.Id;
                        p.Count = 5;
                    })
                ];
            });

        // Act
        await certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        Context.Set<CertificateProduct>()
            .Should()
            .ContainSingle(x =>
                x.CertificateId == certificate.Id &&
                x.ProductId == product.Id &&
                x.Count == 5);
    }

    /// <summary>
    /// Проверяет добавление новой позиции продукта при обновлении сертификата с несуществующим продуктом
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task UpdateCertificateShouldThrowNotFoundExceptionWhenNewProductDoesNotExist()
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

        Context.AddRange(
            seller,
            purchaser,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                    p =>
                    {
                        p.Id = Guid.Empty;
                        p.ProductId = Guid.NewGuid();
                    })
                ];
            });

        // Act
        var act = () => certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
    }

    /// <summary>
    /// Проверяет удаление сертификата
    /// </summary>
    [Fact]
    public async Task DeleteCertificateShouldWork()
    {
        // Arrange
        var seller = TestEntityProvider.Shared.Create<Company>();
        var purchaser = TestEntityProvider.Shared.Create<Company>();

        var entity = TestEntityProvider.Shared.Create<Certificate>(
            x =>
            {
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
            });

        Context.AddRange(seller, purchaser, entity);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        await certificateService.DeleteCertificateAsync(
            entity.Id,
            CancellationToken.None);

        // Assert
        var result = await Context.Set<Certificate>()
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entity.Id);

        result.Should().NotBeNull();
        result!.DeletedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Проверяет удаление несуществующего сертификата
    /// </summary>
    [Fact]
    public async Task DeleteCertificateShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => certificateService.DeleteCertificateAsync(
            id,
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Certificate>>();
    }

    /// <summary>
    /// Проверяет удаление позиции продукта, отсутствующей в модели обновления
    /// </summary>
    [Fact]
    public async Task UpdateCertificateShouldDeleteMissingProductItems()
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

        var productItem1 = TestEntityProvider.Shared.Create<CertificateProduct>(
            x =>
            {
                x.CertificateId = certificate.Id;
                x.ProductId = product1.Id;
            });

        var productItem2 = TestEntityProvider.Shared.Create<CertificateProduct>(
            x =>
            {
                x.CertificateId = certificate.Id;
                x.ProductId = product2.Id;
            });

        certificate.ProductItems.Add(productItem1);
        certificate.ProductItems.Add(productItem2);

        Context.AddRange(
            seller,
            purchaser,
            product1,
            product2,
            certificate);

        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var updateModel = TestEntityProvider.Shared.Create<CertificateUpdateModel>(
            x =>
            {
                x.Id = certificate.Id;
                x.SellerId = seller.Id;
                x.PurchaserId = purchaser.Id;
                x.Products =
                [
                    TestEntityProvider.Shared.Create<CertificateProductUpdateModel>(
                    p =>
                    {
                        p.Id = productItem1.Id;
                        p.ProductId = product1.Id;
                    })
                ];
            });

        // Act
        await certificateService.UpdateCertificateAsync(
            updateModel,
            CancellationToken.None);

        // Assert
        var result = await Context.Set<CertificateProduct>()
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productItem2.Id);

        result.Should().NotBeNull();
        result!.DeletedAt.Should().NotBeNull();
    }

}
