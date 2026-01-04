using Domain.SeoFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SeoFiles
{
    public class SeoFileConfiguration : IEntityTypeConfiguration<SeoFile>
    {
        public void Configure(EntityTypeBuilder<SeoFile> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Name).HasMaxLength(100);
            //builder.Property(c => c.FileGuid).HasMaxLength(100);
            builder.Property(c => c.FileType).HasMaxLength(100);
        }
    }
}
