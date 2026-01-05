using Domain.Spaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Spaces
{
    public class SpacerConfiguration : IEntityTypeConfiguration<Space>
    {
        public void Configure(EntityTypeBuilder<Space> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Location).HasMaxLength(4000);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsActive).HasDefaultValue(true);

        }
    }
}
