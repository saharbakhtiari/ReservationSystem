using Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.News
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Ignore(c => c.Repository);
            builder.Ignore(c => c.DomainService);
            builder.Property(c => c.ApplicationId);
            builder.Property(c => c.Title).HasMaxLength(300);
            builder.Property(c => c.ImageUrl).HasMaxLength(1000);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
