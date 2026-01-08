using Domain.BookingParticipants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BookingParticipants
{
    public class BookingParticipantConfiguration : IEntityTypeConfiguration<BookingParticipant>
    {
        public void Configure(EntityTypeBuilder<BookingParticipant> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            // builder.Property(c => c.Rules).HasMaxLength(4000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
