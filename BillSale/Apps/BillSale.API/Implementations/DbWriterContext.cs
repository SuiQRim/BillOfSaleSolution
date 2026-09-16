using BillSale.Common;
using BillSale.DAL.Contracts.Repositories;

namespace BillSale.API.Implementations
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    /// <param name="writer">Писатель для работы с базой данных</param>
    /// <param name="dateTimeProvider">Провайдер даты и времени</param>
    /// <param name="identityProvider">Провайдер идентичности</param>
    public class DbWriterContext(IWriter writer, IDateTimeProvider dateTimeProvider, IIdentityProvider identityProvider) : IDbWriterContext
    {
        /// <summary>
        /// Писатель для работы с базой данных
        /// </summary>
        public IWriter Writer => writer;

        /// <summary>
        /// Провайдер даты и времени
        /// </summary>
        public IDateTimeProvider DateTimeProvider => dateTimeProvider;

        /// <summary>
        /// Провайдер идентичности
        /// </summary>
        public IIdentityProvider IdentityProvider => identityProvider;
    }
}
