using BillSale.Context.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillSale.Entities.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="CertificateProductConfiguration"/> для Entity Framework Core
    /// </summary>
    internal class CertificateProductConfiguration : IEntityTypeConfiguration<CertificateProduct>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<CertificateProduct> builder)
        {
            builder.ToTable("CertificateProduct", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_CertificateProduct_Count_Range",
                    "\"Count\" BETWEEN 1 AND 100000");
            });

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
