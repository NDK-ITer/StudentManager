using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(200);
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Birthday).IsRequired(true);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.FirstEmail).IsRequired(true).HasMaxLength(70);
            builder.Property(x => x.PresentEmail).IsRequired(true).HasMaxLength(70);
            builder.Property(x => x.PasswordHash).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.RoleId).HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Avatar).HasMaxLength(100);
            builder.Property(x => x.LinkAvatar).HasMaxLength(100);

            builder.HasOne<Role>(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(fk => fk.RoleId);
        }
    }
}
