using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteka.Models.EntityMapper
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_userId");
            builder.HasIndex(x => x.Username)
                .IsUnique();
            builder.HasIndex(x => x.PasswordSalt)
                .IsUnique();

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                   .HasColumnType("INT");
            builder.Property(x => x.Username)
                .HasColumnName("username")
                   .HasColumnType("NVARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.PasswordHash)
                .HasColumnName("passwordHash")
                   .HasColumnType("CHAR(64)")
                   .IsRequired();
            builder.Property(x => x.PasswordSalt)
                .HasColumnName("passwordSalt")
                   .HasColumnType("CHAR(32)")
                   .IsRequired();
        }
    }
}
