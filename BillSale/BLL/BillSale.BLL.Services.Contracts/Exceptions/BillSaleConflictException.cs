namespace BillSale.BLL.Services.Contracts.Exceptions
{
    public class BillSaleConflictException : BillSaleException
    {
        public BillSaleConflictException(string message)
            : base(message)
        {
        }
    }
}
