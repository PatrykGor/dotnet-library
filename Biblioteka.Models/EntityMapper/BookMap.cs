using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteka.Models.EntityMapper
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_bookId");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                    .HasColumnType("INT");
            builder.Property(x => x.Title)
                .HasColumnName("title")
                   .HasColumnType("NVARCHAR(100)")
                    .IsRequired();
            builder.Property(x => x.Author)
                .HasColumnName("author")
                   .HasColumnType("NVARCHAR(100)")
                    .IsRequired();
            builder.Property(x => x.PublishingDate)
                  .HasColumnName("publishing_date")
                    .HasColumnType("datetime")
                    .IsRequired();
        }
    }
}
