using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.Key).HasMaxLength(200);
            builder.HasIndex(c => c.Key).IsUnique();
            builder.Property(c => c.Value).HasMaxLength(4000);
            builder.Property(c => c.RowVersion).IsRowVersion();
        }
    }
}
