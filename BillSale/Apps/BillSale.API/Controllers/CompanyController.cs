using AutoMapper;
using BillSale.API.Models.Company;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.Common;
using Microsoft.AspNetCore.Mvc;

namespace BillSale.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с сущностью компания
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="companyService">Сервис компаний</param>
        /// <param name="mapper">Маппер для преобразования моделей</param>
        /// <param name="validateService">Сервис валидации</param>
        public CompanyController(ICompanyService companyService, IMapper mapper, IValidateService validateService)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получить список компаний
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CompanyApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
        {
            var companies = await companyService.GetCompaniesAsync(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<CompanyApiModel>>(companies));
        }

        /// <summary>
        /// Получить компанию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Компания</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CompanyApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompany(Guid id, CancellationToken cancellationToken)
        {
            var company = await companyService.GetCompanyByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CompanyApiModel>(company));
        }

        /// <summary>
        /// Создать компанию
        /// </summary>
        /// <param name="companyModel">Модель компании</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданная компания</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateApiModel companyModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<CompanyCreateModel>(companyModel);
            await validateService.ValidateAsync(entity, cancellationToken);

            var createdCompany = await companyService.AddCompanyAsync(entity, cancellationToken);
            return Ok(mapper.Map<CompanyApiModel>(createdCompany));
        }

        /// <summary>
        /// Обновить компанию
        /// </summary>
        /// <param name="companyModel">Модель компании</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyApiModel companyModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<CompanyUpdateModel>(companyModel);
            await validateService.ValidateAsync(entity, cancellationToken);

            await companyService.UpdateCompanyAsync(entity, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить компанию
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompany(Guid id, CancellationToken cancellationToken)
        {
            await companyService.DeleteCompanyAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
