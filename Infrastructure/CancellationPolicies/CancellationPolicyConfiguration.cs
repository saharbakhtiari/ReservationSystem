using Domain.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Tariffs
{
    public class TariffrConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
           // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
