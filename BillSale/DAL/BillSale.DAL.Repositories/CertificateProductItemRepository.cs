using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью <see cref="CertificateProduct"/>
    /// </summary>
    /// <remarks>
    /// ctor
    /// </remarks>
    /// <param name="writerContext">писатель</param>
    public class CertificateProductItemRepository(
        IDbWriterContext writerContext)
        : BaseWriteRepository<CertificateProduct>(writerContext),
      ICertificateProductItemRepository
    {
    }
}
