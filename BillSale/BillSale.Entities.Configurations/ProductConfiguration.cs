using BillSale.Context.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillSale.Entities.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="TransferCertificate"/> для Entity Framework Core
    /// </summary>
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasIdAsKey();
            builder.CreateAuditConfiguration();
            builder.UpdateAuditConfiguration();
            builder.CreateSoftDeleteConfiguration();

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(60);

            builder.Property(x => x.MeasureUnit)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
