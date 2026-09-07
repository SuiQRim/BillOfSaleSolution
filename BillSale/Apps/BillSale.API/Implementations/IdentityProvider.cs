using BillSale.Common;

namespace BillSale.API.Implementations
{
    public class IdentityProvider : IIdentityProvider
    {
        public string Name => "Admin";
    }
}
