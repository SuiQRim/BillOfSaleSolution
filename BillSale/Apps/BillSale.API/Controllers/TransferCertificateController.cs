using AutoMapper;
using BillSale.API.Models.Certificate;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using Microsoft.AspNetCore.Mvc;

namespace BillSale.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с актами передачи
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransferCertificateController : ControllerBase
    {
        private readonly ITransferCertificateService certificateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public TransferCertificateController(ITransferCertificateService certificateService, IMapper mapper)
        {
            this.certificateService = certificateService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificatesAsync(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<CertificateApiModel>>(certificates));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateApiModel>(certificates));
        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> GetDetailsById(Guid id, CancellationToken cancellationToken)
        {
            var certificates = await certificateService.GetDetailCertificateByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CertificateDetailsApiModel>(certificates));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CertificateCreateApiModel model, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CertificateCreateModel>(model);

            var result = await certificateService.AddCertificateAsync(mapped, cancellationToken);
            return Ok(mapper.Map<CertificateDetailsApiModel>(result));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CertificateUpdateApiModel model, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CertificateUpdateModel>(model);
            await certificateService.UpdateCertificateAsync(mapped, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await certificateService.DeleteCertificateAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
