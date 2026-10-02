using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BillSale.API.Models.Company;
using BillSale.API.Tests.Infrastructure;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.API.Tests
{
    /// <summary>
    /// Интеграционные тесты для контроллера компаний
    /// </summary>
    [Collection(nameof(BillsalseApiTestCollection))]
    public class CompanyControllerTests : IntegrationTestBase
    {
        private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fixture">Фикстура для интеграционных тестов</param>
        public CompanyControllerTests(BillSaleApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение списка компаний
        /// </summary>
        [Fact]
        public async Task GetCompanies_ShouldReturnCompanies()
        {
            var expected = await SeedCompanies();

            var client = CreateClient();
            var response = await client.GetAsync("api/company");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var companies = await response.Content
                .ReadFromJsonAsync<List<CompanyApiModel>>(jsonOptions);

            Assert.NotNull(companies);

            foreach (var expectedCompany in expected)
            {
                var actualCompany = companies.Single(x => x.Id == expectedCompany.Id);

                Assert.Equal(expectedCompany.OrganizationName, actualCompany.OrganizationName);
            }
        }

        /// <summary>
        /// Тест на получение компании по идентификатору
        /// </summary>
        [Fact]
        public async Task GetCompanyById_ShouldReturnOk()
        {
            var expected = await SeedCompanies();
            var company = expected[0];

            var client = CreateClient();
            var response = await client.GetAsync($"api/company/{company.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actualCompany = await response.Content
                .ReadFromJsonAsync<CompanyApiModel>(jsonOptions);

            Assert.NotNull(actualCompany);
            Assert.Equal(company.Id, actualCompany.Id);
            Assert.Equal(company.OrganizationName, actualCompany.OrganizationName);
        }

        /// <summary>
        /// Тест на получение компании по несуществующему идентификатору
        /// </summary>
        [Fact]
        public async Task GetCompanyById_WhenNotFound_ShouldReturn404()
        {
            var nonExistentCompanyId = Guid.NewGuid();

            var client = CreateClient();
            var response = await client.GetAsync($"api/company/{nonExistentCompanyId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест на создание компании с валидными данными
        /// </summary>
        [Fact]
        public async Task CreateCompany_WhenValidRequest_ShouldReturnOk()
        {
            var newCompany = new CompanyCreateApiModel
            {
                OrganizationName = "Новая компания",
                DocumentName = "Документ",
                FirstName = "Имя",
                LastName = "Фамилия",
                MiddleName = "Отчество",
                Post = "Должность"
            };

            var client = CreateClient();
            var response = await client.PostAsJsonAsync("api/company", newCompany);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createdCompany = await response.Content
                .ReadFromJsonAsync<CompanyApiModel>(jsonOptions);

            Assert.NotNull(createdCompany);
            Assert.NotEqual(Guid.Empty, createdCompany.Id);
            Assert.Equal(newCompany.OrganizationName, createdCompany.OrganizationName);
            Assert.Equal(newCompany.DocumentName, createdCompany.DocumentName);
            Assert.Equal(newCompany.FirstName, createdCompany.FirstName);
            Assert.Equal(newCompany.LastName, createdCompany.LastName);
            Assert.Equal(newCompany.MiddleName, createdCompany.MiddleName);
            Assert.Equal(newCompany.Post, createdCompany.Post);
        }

        /// <summary>
        /// Тест на создание компании с невалидными данными
        /// </summary>
        [Fact]
        public async Task CreateCompany_WhenInvalidData_ShouldReturn422()
        {
            var invalidCompany = new CompanyCreateApiModel
            {
                OrganizationName = ""
            };

            var client = CreateClient();
            var response = await client.PostAsJsonAsync("api/company", invalidCompany);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        /// <summary>
        /// Тест на обновление компании с валидными данными
        /// </summary>
        [Fact]
        public async Task UpdateCompany_WhenValidRequest_ShouldReturnNoContent()
        {
            var existingCompanies = await SeedCompanies();
            var companyToUpdate = existingCompanies[0];

            var updatedCompany = new CompanyApiModel
            {
                Id = companyToUpdate.Id,
                OrganizationName = "Обновленная компания",
                DocumentName = "Обновленный документ",
                FirstName = "Обновленное имя",
                LastName = "Обновленная фамилия",
                MiddleName = "Обновленное отчество",
                Post = "Обновленная должность"
            };

            var client = CreateClient();
            var response = await client.PutAsJsonAsync("api/company", updatedCompany);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var company = await Context.Set<Company>()
                .SingleAsync(x => x.Id == companyToUpdate.Id);

            Assert.Equal(updatedCompany.OrganizationName, company.OrganizationName);
            Assert.Equal(updatedCompany.DocumentName, company.DocumentName);
            Assert.Equal(updatedCompany.FirstName, company.FirstName);
            Assert.Equal(updatedCompany.LastName, company.LastName);
            Assert.Equal(updatedCompany.MiddleName, company.MiddleName);
            Assert.Equal(updatedCompany.Post, company.Post);
        }

        /// <summary>
        /// Тест на обновление компании с несуществующим идентификатором
        /// </summary>
        [Fact]
        public async Task UpdateCompany_WhenNotFound_ShouldReturn404()
        {
            var nonExistentCompanyId = Guid.NewGuid();

            var updatedCompany = new CompanyApiModel
            {
                Id = nonExistentCompanyId,
                OrganizationName = "Обновленная компания",
                DocumentName = "Обновленный документ",
                FirstName = "Обновленное имя",
                LastName = "Обновленная фамилия",
                MiddleName = "Обновленное отчество",
                Post = "Обновленная должность"
            };

            var client = CreateClient();
            var response = await client.PutAsJsonAsync("api/company", updatedCompany);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Тест на обновление компании с невалидными данными
        /// </summary>
        [Fact]
        public async Task UpdateCompany_WhenInvalidData_ShouldReturn422()
        {
            var existingCompanies = await SeedCompanies();
            var companyToUpdate = existingCompanies[0];

            var invalidUpdatedCompany = new CompanyApiModel
            {
                Id = companyToUpdate.Id,
                OrganizationName = ""
            };

            var client = CreateClient();
            var response = await client.PutAsJsonAsync("api/company", invalidUpdatedCompany);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        /// <summary>
        /// Тест на удаление компании с существующим идентификатором
        /// </summary>
        [Fact]
        public async Task DeleteCompany_WhenExists_ShouldReturnNoContent()
        {
            var existingCompanies = await SeedCompanies();
            var companyToDelete = existingCompanies[0];

            var client = CreateClient();
            var response = await client.DeleteAsync($"api/company/{companyToDelete.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Тест на удаление компании с несуществующим идентификатором
        /// </summary>
        [Fact]
        public async Task DeleteCompany_WhenNotFound_ShouldReturn404()
        {
            var nonExistentCompanyId = Guid.NewGuid();

            var client = CreateClient();
            var response = await client.DeleteAsync($"api/company/{nonExistentCompanyId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<List<Company>> SeedCompanies()
        {
            var companies = new List<Company>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrganizationName = "Тестовая компания 1",
                    DocumentName = "Документ 1",
                    FirstName = "Имя 1",
                    LastName = "Фамилия 1",
                    MiddleName = "Отчество 1",
                    Post = "Должность 1"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    OrganizationName = "Тестовая компания 2",
                    DocumentName = "Документ 2",
                    FirstName = "Имя 2",
                    LastName = "Фамилия 2",
                    MiddleName = "Отчество 2",
                    Post = "Должность 2"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    OrganizationName = "Тестовая компания 3",
                    DocumentName = "Документ 3",
                    FirstName = "Имя 3",
                    LastName = "Фамилия 3",
                    MiddleName = "Отчество 3",
                    Post = "Должность 3"
                }
            };

            Context.AddRange(companies);
            await UnitOfWork.SaveChangesAsync();

            return companies;
        }
    }
}
