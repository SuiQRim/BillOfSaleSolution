using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product : BaseAuditEntity
    {
        public Product()
        {
        }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Еденица измерения
        /// </summary>
        public string MeasureUnit { get; set; } = string.Empty;

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Цена за один продукт
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена за все продукты
        /// </summary>
        public decimal TotalPrice => Price * Count;
    }
}
