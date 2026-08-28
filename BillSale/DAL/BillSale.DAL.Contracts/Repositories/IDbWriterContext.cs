

using BillSale.Common;

namespace BillSale.DAL.Contracts.Repositories
{
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
