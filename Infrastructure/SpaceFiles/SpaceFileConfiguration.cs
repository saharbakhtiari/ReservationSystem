using Domain.SpaceFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SpaceFiles
{
    public class SpaceFileConfiguration : IEntityTypeConfiguration<SpaceFile>
    {
        public void Configure(EntityTypeBuilder<SpaceFile> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Name).HasMaxLength(100);
            //builder.Property(c => c.FileGuid).HasMaxLength(100);
            builder.Property(c => c.FileType).HasMaxLength(100);
        }
    }
}
