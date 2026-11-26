namespace BillSale.Entities.Constraints
{
    /// <summary>
    /// Константы содержащие ограничения для продукта
    /// </summary>
    public static class ProductConstraints
    {
        /// <summary>
        /// Максимальная длина наименования продукта
        /// </summary>
        public const int NameMaxLength = 60;

        /// <summary>
        /// Максимальная длина единицы измерения
        /// </summary>
        public const int MeasureUnitMaxLength = 10;

    }
}
