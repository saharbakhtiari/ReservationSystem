using Domain.BookingHolds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BookingHolds
{
    public class BookingHoldConfiguration : IEntityTypeConfiguration<BookingHold>
    {
        public void Configure(EntityTypeBuilder<BookingHold> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
