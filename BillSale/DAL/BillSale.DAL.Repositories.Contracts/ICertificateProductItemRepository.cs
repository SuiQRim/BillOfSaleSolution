using BillSale.DAL.Contracts.Repositories;
using BillSale.Entities;

namespace BillSale.DAL.Repositories.Contracts
{
    /// <summary>
    /// Контракт репозитория продуктов <see cref="TransferCertificateProduct"/> акта
    /// </summary>
    public interface ICertificateProductItemRepository : IBaseWriteRepository<TransferCertificateProduct>
    {

    }
}
