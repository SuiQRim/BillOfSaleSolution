using BillSale.Context.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillSale.Entities.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="Company"/> для Entity Framework Core
    /// </summary>
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasIdAsKey();
            builder.CreateAuditConfiguration();
            builder.UpdateAuditConfiguration();
            builder.CreateSoftDeleteConfiguration();

            builder.Property(x => x.Post)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(60);

            builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(60);

            builder.Property(x => x.MiddleName)
               .IsRequired()
               .HasMaxLength(60);

            builder.Property(x => x.DocumentName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.OrganizationName)
               .IsRequired()
               .HasMaxLength(100);
        }
    }
}
