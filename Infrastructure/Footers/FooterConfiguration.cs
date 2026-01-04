using Domain.Footers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Footers
{
    public class FooterConfiguration : IEntityTypeConfiguration<Footer>
    {
        public void Configure(EntityTypeBuilder<Footer> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Body).HasMaxLength(4000);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsDraft).HasDefaultValue(true);
            builder.OwnsMany(c => c.Links);

        }
    }
}
