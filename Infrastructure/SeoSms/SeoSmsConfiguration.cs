using Domain.SeoSms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SeoSmss
{
    public class SeoSmsConfiguration : IEntityTypeConfiguration<SeoSms>
    {

        public void Configure(EntityTypeBuilder<SeoSms> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.PhoneNumber).HasMaxLength(15);
            builder.Property(c => c.VerifyCode).HasMaxLength(300);
        }
    }
}
