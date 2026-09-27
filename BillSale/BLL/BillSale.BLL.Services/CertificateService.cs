using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.BLL.Services
{
    /// <summary>
    /// Сервис для работы с <see cref="Certificate"/>
    /// </summary>
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository certificateRepository;
        private readonly ICertificateProductItemRepository productItemRepository;
        private readonly IProductRepository productRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="certificateRepository">Репозиторий сертификатов</param>
        /// <param name="productItemRepository">Репозиторий элементов продуктов</param>
        /// <param name="productRepository">Репозиторий продуктов</param>
        /// <param name="companyRepository">Репозиторий компаний</param>
        /// <param name="unitOfWork">Объект единицы работы</param>
        /// <param name="mapper">Маппер</param>
        public CertificateService(ICertificateRepository certificateRepository, ICertificateProductItemRepository productItemRepository,
            IProductRepository productRepository, ICompanyRepository companyRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.certificateRepository = certificateRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.productItemRepository = productItemRepository;
            this.companyRepository = companyRepository;
            this.productRepository = productRepository;
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
                throw new EntityNotFoundException<Certificate>(id);
            }
            return mapper.Map<CertificateModel>(entity);
        }

        /// <inheritdoc />
        public async Task<CertificateDetailModel> GetDetailCertificateByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateDetailById(id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Certificate>(id);
            }
            return mapper.Map<CertificateDetailModel>(entity);
        }

        /// <inheritdoc />
        public async Task UpdateCertificateAsync(CertificateUpdateModel certificateModel, CancellationToken cancellationToken)
        {
            var entity = await UpdateExistAsync(certificateModel, cancellationToken);

            mapper.Map(certificateModel, entity);

            DeleteCertificateProductItems(certificateModel, entity);
            await AddCertificateProductItems(certificateModel, entity, cancellationToken);
            await UpdateProductItems(certificateModel, entity, cancellationToken);

            certificateRepository.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task<Certificate> UpdateExistAsync(CertificateUpdateModel certificateModel, CancellationToken cancellationToken)
        {
            var entity = await certificateRepository.GetCertificateDetailById(certificateModel.Id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Certificate>(certificateModel.Id);
            }

            await CompanyExistAsync(certificateModel.SellerId, certificateModel.PurchaserId, cancellationToken);

            var invalidProduct = certificateModel.Products
                .FirstOrDefault(x =>
                    x.Id != Guid.Empty && !entity.ProductItems.Any(p => p.Id == x.Id));

            if (invalidProduct is not null)
            {
                throw new NotFoundException($"Позиция продукта не существует и не может быть обновлена. Id: {invalidProduct.Id}");
            }

            return entity;
        }

        private async Task CompanyExistAsync(Guid sellerId, Guid purchaserId, CancellationToken cancellationToken)
        {
            var seller = await companyRepository.GetCompanyByIdAsync(sellerId, cancellationToken);
            if (seller is null)
            {
                throw new EntityNotFoundException<Company>(sellerId);
            }

            var purchaser = await companyRepository.GetCompanyByIdAsync(purchaserId, cancellationToken);
            if (purchaser is null)
            {
                throw new EntityNotFoundException<Company>(purchaserId);
            }
        }

        private async Task UpdateProductItems(CertificateUpdateModel certificateModel, Certificate entity, CancellationToken cancellationToken)
        {
            foreach (var productModel in certificateModel.Products.Where(x => x.Id != Guid.Empty))
            {
                var product = await productRepository.GetProductById(productModel.ProductId, cancellationToken);
                if (product is null)
                {
                    throw new EntityNotFoundException<Product>(productModel.ProductId);
                }

                var productItem = entity.ProductItems.First(x => x.Id == productModel.Id);

                mapper.Map(productModel, productItem);
                productItemRepository.Update(productItem);
            }
        }

        private async Task AddCertificateProductItems(CertificateUpdateModel certificateModel, Certificate entity, CancellationToken cancellationToken)
        {
            foreach (var productModel in certificateModel.Products.Where(x => x.Id == Guid.Empty))
            {
                var product = await productRepository.GetProductById(productModel.ProductId, cancellationToken);
                if (product is null)
                {
                    throw new EntityNotFoundException<Product>(productModel.ProductId);
                }

                var productItem = mapper.Map<CertificateProduct>(productModel);

                productItem.CertificateId = entity.Id;

                productItemRepository.Add(productItem);

            }
        }

        private void DeleteCertificateProductItems(CertificateUpdateModel certificateModel, Certificate entity)
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
            await CompanyExistAsync(certificateModel.SellerId, certificateModel.PurchaserId, cancellationToken);
            var entity = mapper.Map<Certificate>(certificateModel);

            certificateRepository.Add(entity);

            foreach (var productModel in entity.ProductItems)
            {
                var product = await productRepository.GetProductById(productModel.ProductId, cancellationToken);
                if (product is null)
                {
                    throw new EntityNotFoundException<Product>(productModel.ProductId);
                }

                productModel.CertificateId = entity.Id;
                productItemRepository.Add(productModel);
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
                throw new EntityNotFoundException<Certificate>(id);
            }

            certificateRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
