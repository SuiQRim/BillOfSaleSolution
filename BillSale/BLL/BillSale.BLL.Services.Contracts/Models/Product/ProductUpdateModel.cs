namespace BillSale.BLL.Services.Contracts.Models.Product
{
    /// <summary>
    /// Модель обновления продукта
    /// </summary>
    public class ProductUpdateModel
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
