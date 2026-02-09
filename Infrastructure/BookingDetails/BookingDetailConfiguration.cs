using Domain.BookingDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BookingDetails
{
    public class BookingDetailConfiguration : IEntityTypeConfiguration<BookingDetail>
    {
        public void Configure(EntityTypeBuilder<BookingDetail> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
