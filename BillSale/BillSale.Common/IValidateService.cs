namespace BillSale.Common
{
    public interface IValidateService
    {
        Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}
