using Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Bookings
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
           // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
