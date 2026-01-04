using Domain.Cartables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Cartables
{
    public class CartableConfiguration : IEntityTypeConfiguration<Cartable>
    {

        public void Configure(EntityTypeBuilder<Cartable> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Title).HasMaxLength(4000);
            builder.HasMany(c => c.Users).WithMany(x => x.Cartables);
        }
    }
}
