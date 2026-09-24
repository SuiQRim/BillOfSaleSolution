using BillSale.Context.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillSale.Entities.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="Certificate"/> для Entity Framework Core
    /// </summary>
    internal class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificates");
            builder.HasIdAsKey();
            builder.CreateAuditConfiguration();
            builder.UpdateAuditConfiguration();
            builder.CreateSoftDeleteConfiguration();

            builder.Property(x => x.City)
               .IsRequired()
               .HasMaxLength(100);

            builder.HasOne(tc => tc.Seller)
                .WithMany()
                .HasForeignKey(tc => tc.SellerId)
                .IsRequired();

            builder.HasOne(tc => tc.Purchaser)
                .WithMany()
                .HasForeignKey(tc => tc.PurchaserId)
                .IsRequired();

            builder.HasMany(x => x.ProductItems)
                .WithOne()
                .HasForeignKey(x => x.CertificateId)
                .IsRequired();
        }
    }
}
