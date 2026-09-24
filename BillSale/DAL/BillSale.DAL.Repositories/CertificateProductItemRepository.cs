using BillSale.DAL.Context.Repositories;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="CertificateProduct"/>
/// </summary>
public class TransferCertificateProductRepository
    : BaseWriteRepository<CertificateProduct>,
      ICertificateProductItemRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="writerContext">писатель</param>
    public TransferCertificateProductRepository(
        IDbWriterContext writerContext)
        : base(writerContext)
    {
    }
}
