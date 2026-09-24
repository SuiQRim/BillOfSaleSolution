using BillSale.Common;

namespace BillSale.API.Implementations
{
    /// <summary>
    /// Реализация провайдера даты и времени
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Получает текущее время в формате UTC
        /// </summary>
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        /// <summary>
        /// Получает текущее локальное время
        /// </summary>
        public DateTime LocalNow => DateTime.Now;
    }
}
