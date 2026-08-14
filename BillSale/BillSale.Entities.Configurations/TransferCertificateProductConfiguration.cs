using BillSale.Context.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillSale.Entities.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="TransferCertificateProductConfiguration"/> для Entity Framework Core
    /// </summary>
    internal class TransferCertificateProductConfiguration : IEntityTypeConfiguration<TransferCertificateProduct>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<TransferCertificateProduct> builder)
        {
            builder.ToTable("TransferCertificateProduct");
            builder.HasIdAsKey();
            builder.CreateAuditConfiguration();
            builder.UpdateAuditConfiguration();
            builder.CreateSoftDeleteConfiguration();

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Count)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
