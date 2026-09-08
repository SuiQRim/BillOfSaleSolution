using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.BLL.Services
{
    /// <summary>
    /// Сервис для работы с <see cref="TransferCertificate"/>
    /// </summary>
    public class TransferCertificateService : ITransferCertificateService
    {
        private readonly ITransferCertificateRepository certificateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="certificateRepository">Репозиторий сущности</param>
        /// <param name="unitOfWork">Обьект еденицы работы</param>
        /// <param name="mapper">маппер</param>
        public TransferCertificateService(ITransferCertificateRepository certificateRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.certificateRepository = certificateRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CertificateModel>> GetCertificatesAsync(CancellationToken cancellationToken)
        {
            var certificates = await certificateRepository.GetCertificatesAsync(cancellationToken);
            return mapper.Map<IReadOnlyCollection<CertificateModel>>(certificates);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<CertificateDetailModel> GetDetailCertificateByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateDetailById(id, cancellationToken);
            return mapper.Map<CertificateDetailModel>(entity);
        }

        /// <inheritdoc />
        public async Task UpdateCertificateAsync(CertificateUpdateModel certificateModel, CancellationToken cancellationToken)
        {
            // TODO: Сделать нормальное обновление для ProductItem
            var entity = await certificateRepository.GetCertificateById(certificateModel.Id, cancellationToken);
            if (entity is null)
            {
                // TODO: Заменить на кастомные
                throw new Exception($"Такого сертификата нет id - {certificateModel.Id}");
            }

            mapper.Map(certificateModel, entity);
            certificateRepository.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<CertificateDetailModel> AddCertificateAsync(CertificateCreateModel certificateModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<TransferCertificate>(certificateModel);
            certificateRepository.Add(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return await GetDetailCertificateByIdAsync(entity.Id, cancellationToken);
        }

        /// <inheritdoc />
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
