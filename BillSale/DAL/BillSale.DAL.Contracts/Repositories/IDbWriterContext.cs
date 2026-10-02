using BillSale.Common;

namespace BillSale.DAL.Contracts.Repositories
{
    /// <summary>
    /// Контракт контекста для записи в базу данных
    /// </summary>
    public interface IDbWriterContext
    {
        /// <inheritdoc cref="IWriter"/>
        IWriter Writer { get; }

        /// <inheritdoc cref="IDateTimeProvider"/>
        IDateTimeProvider DateTimeProvider { get; }

        /// <inheritdoc cref="IIdentityProvider"/>
        IIdentityProvider IdentityProvider { get; }
    }
}
