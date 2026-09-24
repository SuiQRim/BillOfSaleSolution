using AutoMapper;
using BillSale.API.Models.Certificate;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.Common;
using Microsoft.AspNetCore.Mvc;

namespace BillSale.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с актами передачи
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService certificateService;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="certificateService">Сервис актов</param>
        /// <param name="mapper">Маппер для преобразования моделей</param>
        /// <param name="validateService">Сервис валидации</param>
        public CertificateController(ICertificateService certificateService, IMapper mapper, IValidateService validateService)
        {
            this.certificateService = certificateService;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получить список всех актов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CertificateApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificatesAsync(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<CertificateApiModel>>(certificates));
        }

        /// <summary>
        /// Получить сертификат по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список актов</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CertificateApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateApiModel>(certificates));
        }

        /// <summary>
        /// Получить детальную информацию об акте
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Детальный акт</returns>
        [HttpGet("details/{id:guid}")]
        [ProducesResponseType(typeof(CertificateDetailsApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailsById(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetDetailCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateDetailsApiModel>(certificates));
        }

        /// <summary>
        /// Создать акт
        /// </summary>
        /// <param name="model">Форма акта</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Созданный акт</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CertificateDetailsApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CertificateCreateApiModel model, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CertificateCreateModel>(model);
            await validateService.ValidateAsync(mapped, cancellationToken);

            var result = await certificateService.AddCertificateAsync(mapped, cancellationToken);
            return Ok(mapper.Map<CertificateDetailsApiModel>(result));
        }

        /// <summary>
        /// Обновить акт
        /// </summary>
        /// <param name="model">Форма акта</param>
        /// <param name="cancellationToken"></param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update([FromBody] CertificateUpdateApiModel model, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CertificateUpdateModel>(model);
            await validateService.ValidateAsync(mapped, cancellationToken);

            await certificateService.UpdateCertificateAsync(mapped, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить акт по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await certificateService.DeleteCertificateAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
