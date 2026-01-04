using Domain.Sliders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Sliders
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Body).HasMaxLength(4000);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsPublished).HasDefaultValue(false);
            builder.Property(c => c.Link).HasMaxLength(300);

        }
    }
}
