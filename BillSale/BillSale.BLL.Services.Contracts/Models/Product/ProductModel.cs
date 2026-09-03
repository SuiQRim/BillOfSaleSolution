namespace BillSale.BLL.Services.Contracts.Models.Product
{
    /// <summary>
    /// Модель создания продукта
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Еденица измерения
        /// </summary>
        public string MeasureUnit { get; set; } = string.Empty;
    }
}
