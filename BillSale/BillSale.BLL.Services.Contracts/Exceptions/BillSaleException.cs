namespace BillSale.BLL.Services.Contracts.Exceptions
{
    /// <summary>
    /// Общая ошибка
    /// </summary>
    public class BillSaleException : Exception
    {
        /// <summary>
        /// ctor без параметров
        /// </summary>
        public BillSaleException()
        {
        }

        /// <summary>
        /// ctor с передачей сообщения
        /// </summary>
        /// <param name="message">сообщение</param>
        public BillSaleException(string message) : base(message)
        {
        }
    }
}
