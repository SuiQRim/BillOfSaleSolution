using AutoMapper;
using BillSale.API.Models.Product;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Product;
using BillSale.Common;
using Microsoft.AspNetCore.Mvc;

namespace BillSale.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с продуктами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="productService">Сервис для работы с продуктами</param>
        /// <param name="mapper">Маппер для преобразования моделей</param>
        /// <param name="validateService">Сервис для валидации данных</param>
        public ProductController(IProductService productService, IMapper mapper, IValidateService validateService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список продуктов</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ProductApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await productService.GetProductsAsync(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<ProductApiModel>>(products));
        }

        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Продукт</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await productService.GetProductByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<ProductApiModel>(product));
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        /// <param name="productModel">Модель нового продукта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Новый продукт</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateApiModel productModel, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ProductCreateModel>(productModel);
            await validateService.ValidateAsync(mapped, cancellationToken);

            var product = await productService.AddProductAsync(mapped, cancellationToken);
            return Ok(mapper.Map<ProductApiModel>(product));
        }

        /// <summary>
        /// Обновление существующего продукта
        /// </summary>
        /// <param name="productModel">Модель продукта для обновления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductApiModel productModel, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ProductUpdateModel>(productModel);
            await validateService.ValidateAsync(mapped, cancellationToken);

            await productService.UpdateProductAsync(mapped, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удаление продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await productService.DeleteProductAsync(id, cancellationToken);
            return NoContent();
        }

    }
}
