using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.BLL.Services
{
    public class TransferCertificateService : ITransferCertificateService
    {
        private readonly ITransferCertificateRepository certificateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TransferCertificateService(ITransferCertificateRepository certificateRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.certificateRepository = certificateRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CertificateModel>> GetCertificatesAsync(CancellationToken cancellationToken)
        {
            var certificates = await certificateRepository.GetCertificatesAsync(cancellationToken);
            return mapper.Map<IReadOnlyCollection<CertificateModel>>(certificates);
        }

        public async Task<CertificateModel> GetCertificateByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateById(id, cancellationToken);
            if (entity is null)
            {
                // TODO: Заменить на кастомные
                throw new Exception($"Такого сертификата нет id - {id}");
            }
            return mapper.Map<CertificateModel>(entity);

        }

        public async Task<CertificateDetailModel> GetDetailCertificateByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateById();
        }

        public Task UpdateCertificateAsync(CertificateUpdateModel certificateModel, CancellationToken cancellationToken) => throw new NotImplementedException();


        public async Task AddCertificateAsync(CertificateCreateModel certificateModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<TransferCertificate>(certificateModel);
            certificateRepository.Add(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCertificateAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateById(id, cancellationToken);
            if (entity is null)
            {
                // TODO: Заменить на кастомные
                throw new Exception($"Невозможно удалить. Такого сертификата нет id - {id}");
            }

            certificateRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

       
    }
}
