namespace BillSale.Common
{
    /// <summary>
    /// Контракт сервиса валидации
    /// </summary>
    public interface IValidateService
    {
        Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}
