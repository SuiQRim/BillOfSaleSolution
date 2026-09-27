using Ahatornn.TestGenerator;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BillSale.DAL.Repositories.Tests
{
    /// <summary>
    /// Класс тестов проверяющих <see cref="CertificateProductItemRepository"/>
    /// </summary>
    public class CertificateProductItemRepositoryTests : BillSaleContextInMemory
    {
        private readonly ICertificateProductItemRepository certificateProductItemRepository;

        public CertificateProductItemRepositoryTests()
        {
            certificateProductItemRepository =
                new CertificateProductItemRepository(WriterContext);
        }

        /// <summary>
        /// Проверяет метод <see cref="IBaseWriteRepository{T}.Add"/>,
        /// что новый продукт акта успешно добавляется в базу данных.
        /// </summary>
        [Fact]
        public async Task AddShouldCreateCertificateProduct()
        {
            // Arrange
            var certificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>();

            // Act
            certificateProductItemRepository.Add(certificateProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await Context.Set<CertificateProduct>()
                .FindAsync(certificateProduct.Id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(certificateProduct.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="IBaseWriteRepository{T}.Update"/>,
        /// что изменения существующего продукта акта успешно сохраняются
        /// в базе данных.
        /// </summary>
        [Fact]
        public async Task UpdateShouldUpdateCertificateProduct()
        {
            // Arrange
            var certificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>(
                x =>
                {
                    x.Count = 5;
                    x.Price = 100;
                });

            certificateProductItemRepository.Add(certificateProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            certificateProduct.Count = 10;
            certificateProduct.Price = 250;

            // Act
            certificateProductItemRepository.Update(certificateProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await Context.Set<CertificateProduct>()
                .FindAsync(certificateProduct.Id);

            result.Should().NotBeNull();
            result!.Count.Should().Be(10);
            result.Price.Should().Be(250);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICertificateProductItemRepository.Delete"/>,
        /// что после удаления продукт акта помечается как удалённый.
        /// </summary>
        [Fact]
        public async Task DeleteShouldMarkCertificateProductAsDeleted()
        {
            // Arrange
            var certificateProduct = TestEntityProvider.Shared.Create<CertificateProduct>();

            certificateProductItemRepository.Add(certificateProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            certificateProductItemRepository.Delete(certificateProduct);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await Context.Set<CertificateProduct>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == certificateProduct.Id);

            result.Should().NotBeNull();
            result!.DeletedAt.Should().NotBeNull();
        }
    }
}
