using Ahatornn.TestGenerator;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using FluentAssertions;

namespace BillSale.DAL.Repositories.Tests
{
    /// <summary>
    /// Класс тестов проверяющих <see cref="ProductRepository"/>
    /// </summary>
    public class ProductRepositoryTests : BillSaleContextInMemory
    {
        private readonly IProductRepository productRepository;

        public ProductRepositoryTests()
        {
            productRepository = new ProductRepository(WriterContext, Context);
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductsAsync"/>,
        /// что при отсутствии продуктов в базе возвращается пустая коллекция.
        /// </summary>
        [Fact]
        public async Task GetProductsShouldReturnEmpty()
        {
            // Arrange

            // Act
            var products = await productRepository.GetProductsAsync(CancellationToken.None);

            // Assert
            products.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductsAsync"/>,
        /// что метод возвращает все существующие продукты.
        /// </summary>
        [Fact]
        public async Task GetProductsShouldReturnValue()
        {
            // Arrange
            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();
            var product3 = TestEntityProvider.Shared.Create<Product>();

            Context.AddRange(product1, product2, product3);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var products = await productRepository.GetProductsAsync(CancellationToken.None);

            // Assert
            products.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.ContainSingle(x => x.Id == product1.Id)
                .And.ContainSingle(x => x.Id == product2.Id)
                .And.ContainSingle(x => x.Id == product3.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductsAsync"/>,
        /// что метод не возвращает продукты, помеченные как удалённые.
        /// </summary>
        [Fact]
        public async Task GetProductsShouldNotReturnDeletedProducts()
        {
            // Arrange
            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();
            var deletedProduct = TestEntityProvider.Shared.Create<Product>(
                x => x.DeletedAt = DateTimeOffset.Now);

            Context.AddRange(product1, product2, deletedProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var products = await productRepository.GetProductsAsync(CancellationToken.None);

            // Assert
            products.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainSingle(x => x.Id == product1.Id)
                .And.ContainSingle(x => x.Id == product2.Id)
                .And.NotContain(x => x.Id == deletedProduct.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductByIdAsync"/>,
        /// что метод возвращает существующий продукт по его идентификатору.
        /// </summary>
        [Fact]
        public async Task GetProductByIdShouldReturnValue()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Product>();

            Context.Add(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await productRepository.GetProductByIdAsync(
                product.Id,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductByIdAsync"/>,
        /// что метод возвращает null, если продукт с указанным идентификатором не существует.
        /// </summary>
        [Fact]
        public async Task GetProductByIdShouldReturnNullWhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await productRepository.GetProductByIdAsync(
                id,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.GetProductByIdAsync"/>,
        /// что метод возвращает null для продукта, помеченного как удалённый.
        /// </summary>
        [Fact]
        public async Task GetProductByIdShouldReturnNullForDeletedProduct()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Product>(
                x => x.DeletedAt = DateTimeOffset.Now);

            Context.Add(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await productRepository.GetProductByIdAsync(
                product.Id,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.Add"/>,
        /// что новый продукт успешно добавляется в базу данных.
        /// </summary>
        [Fact]
        public async Task AddShouldCreateProduct()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Product>();

            // Act
            productRepository.Add(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await productRepository.GetProductByIdAsync(
                product.Id,
                CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.Update"/>,
        /// что изменения существующего продукта успешно сохраняются в базе данных.
        /// </summary>
        [Fact]
        public async Task UpdateShouldUpdateProduct()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Product>(
                x =>
                {
                    x.Name = "Старое название";
                    x.MeasureUnit = "шт.";
                });

            productRepository.Add(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            product.Name = "Новое название";
            product.MeasureUnit = "кг";

            // Act
            productRepository.Update(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await productRepository.GetProductByIdAsync(
                product.Id,
                CancellationToken.None);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Новое название");
            result.MeasureUnit.Should().Be("кг");
        }

        /// <summary>
        /// Проверяет метод <see cref="IProductRepository.Delete"/>,
        /// что после удаления продукт не возвращается методами чтения репозитория.
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotReturnProduct()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Product>();

            productRepository.Add(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            productRepository.Delete(product);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await productRepository.GetProductByIdAsync(
                product.Id,
                CancellationToken.None);

            result.Should().BeNull();
        }
    }
}
