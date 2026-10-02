using AutoMapper;
using BillSale.API.Models.Certificate;
using BillSale.API.Models.Product;
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
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService certificateService;
        private readonly ICertificateExcelService certificateExcelService;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="certificateService">Сервис актов</param>
        /// <param name="certificateExcelService">Сервис генерации Excel</param>
        /// <param name="mapper">Маппер для преобразования моделей</param>
        /// <param name="validateService">Сервис валидации</param>
        public CertificateController(ICertificateService certificateService, ICertificateExcelService certificateExcelService, IMapper mapper, IValidateService validateService)
        {
            this.certificateService = certificateService;
            this.certificateExcelService = certificateExcelService;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получить список всех актов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список актов</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CertificateApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCertificates(CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificatesAsync(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<CertificateApiModel>>(certificates));
        }

        /// <summary>
        /// Получить сертификат по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список актов</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CertificateApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCertificate(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateApiModel>(certificates));
        }

        /// <summary>
        /// Получить детальную информацию об акте
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Детальный акт</returns>
        [HttpGet("details/{id:guid}")]
        [ProducesResponseType(typeof(CertificateDetailsApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCertificateDetails(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetDetailCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateDetailsApiModel>(certificates));
        }

        /// <summary>
        /// Получить excel файл информацию об акте
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Детальный акт</returns>
        [HttpGet("{id:guid}/excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCertificateExcel(Guid id, CancellationToken cancellationToken)
        {
            var certificate = await certificateService.GetDetailCertificateByIdAsync(id, cancellationToken);
            var stream = await certificateExcelService.GenerateCertificateExcelAsync(certificate, cancellationToken);
            return File(
                 stream,
                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                 $"certificate-{id}.xlsx");
        }

        /// <summary>
        /// Создать акт
        /// </summary>
        /// <param name="model">Форма акта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный акт</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateCertificate([FromBody] CertificateCreateApiModel model, CancellationToken cancellationToken)
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
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateCertificate([FromBody] CertificateUpdateApiModel model, CancellationToken cancellationToken)
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
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCertificate(Guid id, CancellationToken cancellationToken)
        {
            await certificateService.DeleteCertificateAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
