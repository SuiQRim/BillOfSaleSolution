using Ahatornn.TestGenerator;
using BillSale.DAL.Context.Tests;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;
using FluentAssertions;

namespace BillSale.DAL.Repositories.Tests
{
    /// <summary>
    /// Класс тестов проверяющих <see cref="CompanyRepository"/>
    /// </summary>
    public class CompanyRepositoryTests : BillSaleContextInMemory
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyRepositoryTests()
        {
            companyRepository = new CompanyRepository(WriterContext, Context);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompaniesAsync"/>,
        /// что при отсутствии компаний в базе возвращается пустая коллекция.
        /// </summary>
        [Fact]
        public async Task GetCompaniesShouldReturnEmpty()
        {
            // Arrange

            // Act
            var companies = await companyRepository.GetCompaniesAsync(
                CancellationToken.None);

            // Assert
            companies.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompaniesAsync"/>,
        /// что метод возвращает все существующие компании.
        /// </summary>
        [Fact]
        public async Task GetCompaniesShouldReturnValue()
        {
            // Arrange
            var company1 = TestEntityProvider.Shared.Create<Company>();
            var company2 = TestEntityProvider.Shared.Create<Company>();
            var company3 = TestEntityProvider.Shared.Create<Company>();

            Context.AddRange(company1, company2, company3);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var companies = await companyRepository.GetCompaniesAsync(
                CancellationToken.None);

            // Assert
            companies.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.ContainSingle(x => x.Id == company1.Id)
                .And.ContainSingle(x => x.Id == company2.Id)
                .And.ContainSingle(x => x.Id == company3.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompaniesAsync"/>,
        /// что метод не возвращает компании, помеченные как удалённые.
        /// </summary>
        [Fact]
        public async Task GetCompaniesShouldNotReturnDeletedCompanies()
        {
            // Arrange
            var company1 = TestEntityProvider.Shared.Create<Company>();
            var company2 = TestEntityProvider.Shared.Create<Company>();
            var deletedCompany = TestEntityProvider.Shared.Create<Company>(
                x => x.DeletedAt = DateTimeOffset.Now);

            Context.AddRange(company1, company2, deletedCompany);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var companies = await companyRepository.GetCompaniesAsync(
                CancellationToken.None);

            // Assert
            companies.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainSingle(x => x.Id == company1.Id)
                .And.ContainSingle(x => x.Id == company2.Id)
                .And.NotContain(x => x.Id == deletedCompany.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompanyByIdAsync"/>,
        /// что метод возвращает существующую компанию по её идентификатору.
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdShouldReturnValue()
        {
            // Arrange
            var company = TestEntityProvider.Shared.Create<Company>();

            Context.Add(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync(
                company.Id,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(company.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompanyByIdAsync"/>,
        /// что метод возвращает null, если компания с указанным идентификатором не существует.
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdShouldReturnNullWhenCompanyDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await companyRepository.GetCompanyByIdAsync(
                id,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.GetCompanyByIdAsync"/>,
        /// что метод возвращает null для компании, помеченной как удалённая.
        /// </summary>
        [Fact]
        public async Task GetCompanyByIdShouldReturnNullForDeletedCompany()
        {
            // Arrange
            var company = TestEntityProvider.Shared.Create<Company>(
                x => x.DeletedAt = DateTimeOffset.Now);

            Context.Add(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync(
                company.Id,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.Add"/>,
        /// что новая компания успешно добавляется в базу данных.
        /// </summary>
        [Fact]
        public async Task AddShouldCreateCompany()
        {
            // Arrange
            var company = TestEntityProvider.Shared.Create<Company>();

            // Act
            companyRepository.Add(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await companyRepository.GetCompanyByIdAsync(
                company.Id,
                CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(company.Id);
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.Update"/>,
        /// что изменения существующей компании успешно сохраняются в базе данных.
        /// </summary>
        [Fact]
        public async Task UpdateShouldUpdateCompany()
        {
            // Arrange
            var company = TestEntityProvider.Shared.Create<Company>(
                x =>
                {
                    x.OrganizationName = "Старая организация";
                    x.FirstName = "Иван";
                    x.LastName = "Иванов";
                });

            companyRepository.Add(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            company.OrganizationName = "Новая организация";
            company.FirstName = "Пётр";
            company.LastName = "Петров";

            // Act
            companyRepository.Update(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await companyRepository.GetCompanyByIdAsync(
                company.Id,
                CancellationToken.None);

            result.Should().NotBeNull();
            result!.OrganizationName.Should().Be("Новая организация");
            result.FirstName.Should().Be("Пётр");
            result.LastName.Should().Be("Петров");
        }

        /// <summary>
        /// Проверяет метод <see cref="ICompanyRepository.Delete"/>,
        /// что после удаления компания не возвращается методами чтения репозитория.
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotReturnCompany()
        {
            // Arrange
            var company = TestEntityProvider.Shared.Create<Company>();

            companyRepository.Add(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Act
            companyRepository.Delete(company);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            // Assert
            var result = await companyRepository.GetCompanyByIdAsync(
                company.Id,
                CancellationToken.None);

            result.Should().BeNull();
        }
    }
}
