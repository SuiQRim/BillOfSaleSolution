using BillSale.Common;
using BillSale.DAL.Contracts.Repositories;

namespace BillSale.API.Implementations
{
    public class DbWriterContext(IWriter writer, IDateTimeProvider dateTimeProvider, IIdentityProvider identityProvider) : IDbWriterContext
    {
        public IWriter Writer => writer;

        public IDateTimeProvider DateTimeProvider => dateTimeProvider;

        public IIdentityProvider IdentityProvider => identityProvider;
    }
}
