using System.Net.Http.Json;
using System.Text.Json;
using BillSale.API.Models.Product;
using BillSale.API.Tests.Infrastructure;
using BillSale.Entities;

namespace BillSale.API.Tests
{
    /// <summary>
    /// Интеграционные тесты для контроллера продуктов
    /// </summary>
    [Collection(nameof(BillsalseApiTestCollection))]
    public class ProductControllerTests : IntegrationTestBase
    {
        private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fixture">Фикстура для интеграционных тестов</param>
        public ProductControllerTests(BillSaleApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение списка продуктов
        /// </summary>
        [Fact]
        public async Task GetProducts_ShouldReturnProducts()
        {
            // Arrange
            var expected = await SeedProducts();

            // Act
            var client = CreateClient();
            var response = await client.GetAsync("api/product");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var products = await response.Content.ReadFromJsonAsync<List<ProductApiModel>>(jsonOptions);

            Assert.NotNull(products);

            foreach (var expectedProduct in expected)
            {
                var actualProduct = products.Single(x => x.Id == expectedProduct.Id);

                Assert.Equal(expectedProduct.Name, actualProduct.Name);
                Assert.Equal(expectedProduct.MeasureUnit, actualProduct.MeasureUnit);
            }

        }

        /// <summary>
        /// Тест на получение продукта по идентификатору
        /// </summary>
        [Fact]
        public async Task GetProductById_ShouldReturnOk()
        {
            // Arrange
            var expected = await SeedProducts();
            var product = expected[0];

            // Act
            var client = CreateClient();
            var response = await client.GetAsync($"api/product/{product.Id}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var actualProduct = await response.Content.ReadFromJsonAsync<ProductApiModel>(jsonOptions);

            Assert.NotNull(actualProduct);
            Assert.Equal(product.Id, actualProduct.Id);
            Assert.Equal(product.Name, actualProduct.Name);
            Assert.Equal(product.MeasureUnit, actualProduct.MeasureUnit);
        }

        /// <summary>
        /// Тест на получение продукта по несуществующему идентификатору
        /// </summary>
        [Fact]
        public async Task GetProductById_ShouldReturnNotFound()
        {
            // Arrange
            var nonExistentProductId = Guid.NewGuid();
            // Act
            var client = CreateClient();
            var response = await client.GetAsync($"api/product/{nonExistentProductId}");
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест на создание нового продукта
        /// </summary>
        [Fact]
        public async Task CreateProduct_WhenValidRequest_ReturnsCreatedProduct()
        {
            // Arrange
            var newProduct = new ProductCreateApiModel
            {
                Name = "Новый продукт",
                MeasureUnit = "шт."
            };

            // Act
            var client = CreateClient();
            var response = await client.PostAsJsonAsync("api/product", newProduct);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var createdProduct = await response.Content.ReadFromJsonAsync<ProductApiModel>(jsonOptions);
            Assert.NotNull(createdProduct);
            Assert.Equal(newProduct.Name, createdProduct.Name);
            Assert.Equal(newProduct.MeasureUnit, createdProduct.MeasureUnit);
        }

        /// <summary>
        /// Тест на создание нового продукта с некорректными данными
        /// </summary>
        [Fact]
        public async Task CreateProduct_WhenInvalidData_ShouldReturn422()
        {
            // Arrange
            var invalidProduct = new ProductCreateApiModel
            {
                Name = "",
                MeasureUnit = "шт."
            };

            // Act
            var client = CreateClient();
            var response = await client.PostAsJsonAsync("api/product", invalidProduct);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        /// <summary>
        /// Тест на обновление существующего продукта
        /// </summary>
        [Fact]
        public async Task UpdateProduct_ShouldUpdate_ReturnNoContent()
        {
            // Arrange
            var existingProducts = await SeedProducts();
            var productToUpdate = existingProducts[0];
            var updatedProduct = new ProductApiModel
            {
                Id = productToUpdate.Id,
                Name = "Обновленный продукт",
                MeasureUnit = "шт."
            };

            // Act
            var client = CreateClient();
            var response = await client.PutAsJsonAsync($"api/product", updatedProduct);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Тест на обновление несуществующего продукта
        /// </summary>
        [Fact]
        public async Task UpdateProduct_WhenNotFound_ShouldReturn404()
        {
            // Arrange
            var nonExistentProductId = Guid.NewGuid();
            var updatedProduct = new ProductApiModel
            {
                Id = nonExistentProductId,
                Name = "Обновленный продукт",
                MeasureUnit = "шт."
            };

            // Act
            var client = CreateClient();
            var response = await client.PutAsJsonAsync($"api/product", updatedProduct);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест на обновление продукта с некорректными данными
        /// </summary>
        [Fact]
        public async Task UpdateProduct_WhenInvalidData_ShouldReturn422()
        {
            // Arrange
            var existingProducts = await SeedProducts();
            var productToUpdate = existingProducts[0];
            var invalidUpdatedProduct = new ProductApiModel
            {
                Id = productToUpdate.Id,
                Name = "",
                MeasureUnit = "шт."
            };

            // Act
            var client = CreateClient();
            var response = await client.PutAsJsonAsync($"api/product", invalidUpdatedProduct);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        /// <summary>
        /// Тест на удаление существующего продукта
        /// </summary>
        [Fact]
        public async Task DeleteProduct_ShouldDelete_ReturnNoContent()
        {
            // Arrange
            var existingProducts = await SeedProducts();
            var productToDelete = existingProducts[0];

            // Act
            var client = CreateClient();
            var response = await client.DeleteAsync($"api/product/{productToDelete.Id}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Тест на удаление несуществующего продукта
        /// </summary>
        [Fact]
        public async Task DeleteProduct_WhenNotFound_ShouldReturn404()
        {
            // Arrange
            var nonExistentProductId = Guid.NewGuid();

            // Act
            var client = CreateClient();
            var response = await client.DeleteAsync($"api/product/{nonExistentProductId}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<List<Product>> SeedProducts()
        {
            var products = new List<Product>
            {
                new() { Id = Guid.NewGuid(), Name = "Тестовый продукт 1", MeasureUnit = "шт." },
                new() { Id = Guid.NewGuid(), Name = "Тестовый продукт 2", MeasureUnit = "шт." },
                new() { Id = Guid.NewGuid(), Name = "Тестовый продукт 3", MeasureUnit = "шт." }
            };

            Context.AddRange(products);

            await UnitOfWork.SaveChangesAsync();

            return products;
        }

    }
}
