namespace BillSale.API.Models.Product
{
    /// <summary>
    /// Модель создания продукта
    /// </summary>
    public class ProductApiModel : ProductCreateApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
