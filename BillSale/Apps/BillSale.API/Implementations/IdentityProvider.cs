using BillSale.Common;

namespace BillSale.API.Implementations
{
    /// <summary>
    /// Предоставляет информацию о поставщике идентификации
    /// </summary>
    public class IdentityProvider : IIdentityProvider
    {
        /// <summary>
        /// Получает идентификатор пользователя
        /// </summary>
        public string Name => "Admin";
    }
}
