using Ahatornn.TestGenerator;
using AutoMapper;
using BillSale.BLL.Services.Automapper;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories;
using BillSale.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BillSale.BLL.Services.Tests
{
    /// <summary>
    /// Тесты для <see cref="CompanyService"/>
    /// </summary>
    public class CompanyServiceTests : BillSaleContextInMemory
    {
        private readonly CompanyService companyService;

        /// <summary>
        /// ctor
        /// </summary>
        public CompanyServiceTests()
        {
            var companyRepository = new CompanyRepository(WriterContext, Context);
            var profile = new ServiceProfile();
            var mapper = new MapperConfiguration(
                x => x.AddProfile(profile),
                NullLoggerFactory.Instance)
                .CreateMapper();

            companyService = new CompanyService(
                companyRepository,
                UnitOfWork,
                mapper);
        }

        /// <summary>
        /// Тест на получение всех компаний, когда их нет
        /// </summary>
        [Fact]
        public async Task GetCompaniesAsyncShouldReturnEmpty()
        {
            // Act
            var items = await companyService.GetCompaniesAsync(CancellationToken.None);

            // Assert
            items.Should().NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Тест на получение всех компаний, когда они есть
        /// </summary>
        [Fact]
        public async Task GetCompaniesAsyncShouldReturnCompanies()
        {
            // Arrange
            var item1 = TestEntityProvider.Shared.Create<Company>();
            var item2 = TestEntityProvider.Shared.Create<Company>();
            var item3 = TestEntityProvider.Shared.Create<Company>();

            Context.AddRange(item1, item2, item3);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var items = await companyService.GetCompaniesAsync(CancellationToken.None);

            // Assert
            items.Should().HaveCount(3)
                .And.Contain(x => x.Id == item1.Id)
                .And.Contain(x => x.Id == item2.Id)
                .And.Contain(x => x.Id == item3.Id);
        }

        /// <summary>
        /// Тест на получение компании по Id
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdAsyncShouldReturnCompany()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Company>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await companyService.GetCompanyByIdAsync(
                item.Id,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(item.Id);
        }

        /// <summary>
        /// Тест на получение компании по Id, когда компания удалена
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdAsyncShouldThrowNotFoundException()
        {
            // Act
            var act = () => companyService.GetCompanyByIdAsync(
                Guid.NewGuid(),
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
        }

        /// <summary>
        /// Тест на получение компании по Id, когда компании нет
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdAsyncShouldThrowNotFoundExceptionWhenCompanyDeleted()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Company>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            await companyService.DeleteCompanyAsync(
                item.Id,
                CancellationToken.None);

            var act = () => companyService.GetCompanyByIdAsync(
                item.Id,
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
        }

        /// <summary>
        /// Тест на создание компании
        /// </summary>
        [Fact]
        public async Task AddCompanyAsyncShouldWork()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<CompanyCreateModel>();

            // Act
            var result = await companyService.AddCompanyAsync(
                model,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            Context.Set<Company>()
                .Should()
                .ContainSingle(x => x.Id == result.Id);
        }

        /// <summary>
        /// Тест на обновление компании
        /// </summary>
        [Fact]
        public async Task UpdateCompanyAsyncShouldWork()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Company>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            var updateModel = TestEntityProvider.Shared.Create<CompanyUpdateModel>();
            updateModel.Id = item.Id;

            // Act
            await companyService.UpdateCompanyAsync(
                updateModel,
                CancellationToken.None);

            // Assert
            var updatedItem = await Context.Set<Company>()
                .FindAsync(item.Id);

            updatedItem.Should().NotBeNull();
            updatedItem.OrganizationName.Should().Be(updateModel.OrganizationName);
            updatedItem.Post.Should().Be(updateModel.Post);
            updatedItem.FirstName.Should().Be(updateModel.FirstName);
            updatedItem.LastName.Should().Be(updateModel.LastName);
            updatedItem.MiddleName.Should().Be(updateModel.MiddleName);
            updatedItem.DocumentName.Should().Be(updateModel.DocumentName);
        }

        /// <summary>
        /// Тест на обновление компании, когда компании нет
        /// </summary>
        [Fact]
        public async Task UpdateCompanyAsyncShouldThrowNotFoundException()
        {
            // Arrange
            var updateModel = TestEntityProvider.Shared.Create<CompanyUpdateModel>();

            // Act
            var act = () => companyService.UpdateCompanyAsync(
                updateModel,
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
        }

        /// <summary>
        /// Тест на удаление компании
        /// </summary>
        [Fact]
        public async Task DeleteCompanyAsyncShouldWork()
        {
            // Arrange
            var item = TestEntityProvider.Shared.Create<Company>();
            Context.Add(item);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            await companyService.DeleteCompanyAsync(
                item.Id,
                CancellationToken.None);

            // Assert
            var result = await Context.Set<Company>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id);

            result.Should().NotBeNull();
            result.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Тест на удаление компании, когда компании нет
        /// </summary>
        [Fact]
        public async Task DeleteCompanyAsyncShouldThrowNotFoundException()
        {
            // Act
            var act = () => companyService.DeleteCompanyAsync(
                Guid.NewGuid(),
                CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException<Company>>();
        }
    }
}
