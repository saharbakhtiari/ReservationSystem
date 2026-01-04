using Domain.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Headers
{
    public class HeaderrConfiguration : IEntityTypeConfiguration<Header>
    {
        public void Configure(EntityTypeBuilder<Header> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Body).HasMaxLength(4000);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsDraft).HasDefaultValue(true);
            builder.Property(c => c.Link).HasMaxLength(300);

        }
    }
}
