using Domain.SliderFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SliderFiles
{
    public class SliderFileConfiguration : IEntityTypeConfiguration<SliderFile>
    {
        public void Configure(EntityTypeBuilder<SliderFile> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Name).HasMaxLength(100);
            //builder.Property(c => c.FileGuid).HasMaxLength(100);
            builder.Property(c => c.FileType).HasMaxLength(100);
        }
    }
}
