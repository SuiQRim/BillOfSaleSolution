using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Product;
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
        private readonly ICertificateProductItemRepository productItemRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="certificateRepository">Репозиторий сущности</param>
        /// <param name="unitOfWork">Обьект еденицы работы</param>
        /// <param name="mapper">маппер</param>
        public TransferCertificateService(ITransferCertificateRepository certificateRepository, ICertificateProductItemRepository productItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.certificateRepository = certificateRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.productItemRepository = productItemRepository;
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
            var entity = await certificateRepository.GetCertificateDetailById(certificateModel.Id, cancellationToken);
            if (entity is null)
            {
                // TODO: Заменить на кастомные
                throw new Exception($"Такого сертификата нет id - {certificateModel.Id}");
            }
            var invalidProduct = certificateModel.Products
                .FirstOrDefault(x =>
                    x.Id != Guid.Empty && !entity.ProductItems.Any(p => p.Id == x.Id));

            if (invalidProduct is not null)
            {
                // TODO: Заменить на кастомные
                throw new Exception($"Позиция продукта не существует и не может быть обновлена. Id: {invalidProduct.Id}");
            }

            mapper.Map(certificateModel, entity);

            DeleteCertificateProductItems(certificateModel, entity);

            AddCertificateProductItems(certificateModel, entity);

            UpdateProductItems(certificateModel, entity);

            certificateRepository.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private void UpdateProductItems(CertificateUpdateModel certificateModel, TransferCertificate entity)
        {
            foreach (var productModel in certificateModel.Products.Where(x => x.Id != Guid.Empty))
            {
                var product = entity.ProductItems.First(x => x.Id == productModel.Id);

                mapper.Map(productModel, product);
                productItemRepository.Update(product);
            }
        }

        private void AddCertificateProductItems(CertificateUpdateModel certificateModel, TransferCertificate entity)
        {
            foreach (var productModel in certificateModel.Products.Where(x => x.Id == Guid.Empty))
            {
                var product = mapper.Map<TransferCertificateProduct>(productModel);

                product.TransferCertificateId = entity.Id;

                productItemRepository.Add(product);

            }
        }

        private void DeleteCertificateProductItems(CertificateUpdateModel certificateModel, TransferCertificate entity)
        {
            foreach (var product in entity.ProductItems)
            {
                if (!certificateModel.Products.Any(x => x.Id == product.Id))
                {
                    productItemRepository.Delete(product);
                }
            }
        }

        /// <inheritdoc />
        public async Task<CertificateDetailModel> AddCertificateAsync(CertificateCreateModel certificateModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<TransferCertificate>(certificateModel);

            certificateRepository.Add(entity);

            foreach (var product in entity.ProductItems)
            {
                product.TransferCertificateId = entity.Id;
                productItemRepository.Add(product);
            }

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
