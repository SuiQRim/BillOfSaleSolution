namespace BillSale.API.Models.Product
{
    /// <summary>
    /// Данные для обновления товара.
    /// </summary>
    public class ProductUpdateApiModel
    {
        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Единица измерения.
        /// </summary>
        public string MeasureUnit { get; set; } = string.Empty;
    }
}
