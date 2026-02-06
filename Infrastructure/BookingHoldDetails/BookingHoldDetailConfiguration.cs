using Domain.BookingHoldDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BookingHoldDetails
{
    public class BookingHoldDetailConfiguration : IEntityTypeConfiguration<BookingHoldDetail>
    {
        public void Configure(EntityTypeBuilder<BookingHoldDetail> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
