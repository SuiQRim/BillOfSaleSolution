using Ahatornn.TestGenerator;
using AutoMapper;
using BillSale.BLL.Services.Automapper;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Product;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories;
using BillSale.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BillSale.BLL.Services.Tests
{
    /// <summary>
    /// Тесты для <see cref="ProductService"/>
    /// </summary>
    public class ProductServiceTests : BillSaleContextInMemory
    {
        private readonly ProductService productService;

        /// <summary>
        /// ctor
        /// </summary>
        public ProductServiceTests()
        {
            var productRepository = new ProductRepository(WriterContext, Context);
            var profile = new ServiceProfile();
            var mapper = new MapperConfiguration(x => x.AddProfile(profile), NullLoggerFactory.Instance).CreateMapper();

            var certificateRepository = new CertificateRepository(
                WriterContext,
                Context);

            productService = new ProductService(
                productRepository,
                certificateRepository,
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение всех продуктов, когда их нет
        /// </summary>
        [Fact]
        public async Task GetAllProductsAsyncShouldReturnEmpty()
        {
            // Act
            var items = await productService.GetProductsAsync(CancellationToken.None);

            // Assert
            items.Should().NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Тест на получение всех продуктов, когда они есть
        /// </summary>
        [Fact]
        public async Task GetAllProductsAsyncShouldReturnProducts()
        {
            // Arrange
            var item1 = TestEntityProvider.Shared.Create<Product>();
            var item2 = TestEntityProvider.Shared.Create<Product>();
            var item3 = TestEntityProvider.Shared.Create<Product>();

            Context.AddRange(item1, item2, item3);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var items = await productService.GetProductsAsync(CancellationToken.None);

            // Assert
            items.Should().HaveCount(3)
                .And.Contain(x => x.Id == item1.Id)
                .And.Contain(x => x.Id == item2.Id)
                .And.Contain(x => x.Id == item3.Id);
        }

        /// <summary>
        /// Тест на получение продукта по Id
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsyncShouldReturnProduct()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Product>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await productService.GetProductByIdAsync(item.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(item.Id);
        }

        /// <summary>
        /// Тест на получение продукта по Id, когда продукт удален
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsyncShouldThrowNotFoundExceptionWhenProductDeleted()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Product>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            await productService.DeleteProductAsync(item.Id, CancellationToken.None);

            // Act
            var act = () => productService.GetProductByIdAsync(item.Id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
        }

        /// <summary>
        /// Тест на получение продукта по Id, когда продукта нет
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsyncShouldThrowNotFoundException()
        {
            // Act
            var act = () => productService.GetProductByIdAsync(
                Guid.NewGuid(),
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
        }

        /// <summary>
        /// Тест на создание продукта
        /// </summary>
        [Fact]
        public async Task CreateProductAsyncShouldWork()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<ProductCreateModel>();

            // Act
            var result = await productService.AddProductAsync(item, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            Context.Set<Product>().Should().ContainSingle(x => x.Id == result.Id);
        }

        /// <summary>
        /// Тест на обновление продукта
        /// </summary>
        [Fact]
        public async Task UpdateProductAsyncShouldWork()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Product>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            var updateModel = TestEntityProvider.Shared.Create<ProductUpdateModel>();
            updateModel.Id = item.Id;

            // Act
            await productService.UpdateProductAsync(updateModel, CancellationToken.None);

            // Assert

            var updatedItem = await Context.Set<Product>().FindAsync(item.Id);
            updatedItem.Should().NotBeNull();
            updatedItem.Name.Should().Be(updateModel.Name);
            updatedItem.MeasureUnit.Should().Be(updateModel.MeasureUnit);
        }

        /// <summary>
        /// Тест на обновление продукта, когда продукта нет
        /// </summary>
        [Fact]
        public async Task UpdateProductAsyncShouldThrowNotFoundException()
        {
            // Arrange
            var updateModel = TestEntityProvider.Shared.Create<ProductUpdateModel>();

            // Act
            var act = () => productService.UpdateProductAsync(updateModel, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
        }

        /// <summary>
        /// Тест на удаление продукта
        /// </summary>
        [Fact]
        public async Task DeleteProductAsyncShouldWork()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Product>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            await productService.DeleteProductAsync(item.Id, CancellationToken.None);

            // Assert
            var result = await Context.Set<Product>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id);

            result.Should().NotBeNull();
            result.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление продукта, когда продукта нет
        /// </summary>
        [Fact]
        public async Task DeleteProductAsyncShouldThrowNotFoundException()
        {
            // Act
            var act = () => productService.DeleteProductAsync(
                Guid.NewGuid(),
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Product>>();
        }

        /// <summary>
        /// Запрещает удаление товара только при наличии
        /// активной позиции в активном акте.
        /// </summary>
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async Task DeleteProductShouldRespectCertificateReferences(
            bool certificateDeleted,
            bool productItemDeleted)
        {
            // Arrange
            var seller = TestEntityProvider.Shared.Create<Company>();
            var purchaser = TestEntityProvider.Shared.Create<Company>();

            var product = TestEntityProvider.Shared.Create<Product>(
                x => x.DeletedAt = null);

            var certificate = TestEntityProvider.Shared.Create<Certificate>(
                x =>
                {
                    x.SellerId = seller.Id;
                    x.PurchaserId = purchaser.Id;
                    x.DeletedAt = certificateDeleted
                        ? DateTimeOffset.UtcNow
                        : null;
                });

            var productItem = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.CertificateId = certificate.Id;
                    x.ProductId = product.Id;
                    x.DeletedAt = productItemDeleted
                        ? DateTimeOffset.UtcNow
                        : null;
                });

            certificate.ProductItems.Add(productItem);

            Context.AddRange(seller, purchaser, product, certificate);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var deletionAllowed = certificateDeleted || productItemDeleted;

            // Act
            var act = () => productService.DeleteProductAsync(
                product.Id,
                CancellationToken.None);

            // Assert
            if (deletionAllowed)
            {
                await act.Should().NotThrowAsync();
            }
            else
            {
                await act.Should().ThrowAsync<BillSaleConflictException>()
                    .WithMessage(
                        "Нельзя удалить товар: он используется в активных актах.");
            }

            var result = await Context.Set<Product>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .SingleAsync(x => x.Id == product.Id);

            if (deletionAllowed)
            {
                result.DeletedAt.Should().NotBeNull();
            }
            else
            {
                result.DeletedAt.Should().BeNull();
            }
        }
    }
}
