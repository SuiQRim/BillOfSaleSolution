using BillSale.Common;

namespace BillSale.BLL.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение при попытки валидации
    /// </summary>
    public class BillSaleValidationException : BillSaleException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="errors">Ошибки</param>
        public BillSaleValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
