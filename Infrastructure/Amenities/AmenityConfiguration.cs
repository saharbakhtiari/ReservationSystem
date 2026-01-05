using Domain.Amenitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Amenitys
{
    public class AmenityrConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
