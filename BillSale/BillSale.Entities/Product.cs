using BillSale.DAL.Contracts;

namespace BillSale.Entities
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product : BaseAuditEntity
    {

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
