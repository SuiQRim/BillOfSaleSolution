using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Product;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.BLL.Services
{
    /// <summary>
    /// Сервис для работы с <see cref="Product"/>
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="productRepository">Репозиторий сущности</param>
        /// <param name="unitOfWork">Обьект еденицы работы</param>
        /// <param name="mapper">маппер</param>
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<ProductModel>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsAsync(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ProductModel>>(products);
        }

        /// <inheritdoc/>
        public async Task<ProductModel> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetProductByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            return mapper.Map<ProductModel>(entity);
        }

        /// <inheritdoc/>
        public async Task<ProductModel> AddProductAsync(ProductCreateModel productModel, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(productModel);
            productRepository.Add(product);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProductModel>(product);
        }

        /// <inheritdoc/>
        public async Task UpdateProductAsync(ProductModel productModel, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetProductByIdAsync(productModel.Id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Product>(productModel.Id);
            }

            mapper.Map(productModel, entity);
            productRepository.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetProductByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            productRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
