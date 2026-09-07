using AutoMapper;
using BillSale.API.Models;
using BillSale.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BillSale.API.Controllers
{
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
            return Ok(certificates);
        }
    }
}
