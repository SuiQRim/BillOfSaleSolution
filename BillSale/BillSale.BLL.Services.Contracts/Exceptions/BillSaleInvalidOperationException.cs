namespace BillSale.BLL.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение при выполнении операции
    /// </summary>
    /// <param name="message">Сообщение</param>
    public class BillSaleInvalidOperationException(string message) : BillSaleException(message);
}
