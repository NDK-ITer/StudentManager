using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(200);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ListImage)
                .HasConversion(
                    v => string.Join(',', v), // Chuyển List<string> thành một chuỗi ngăn cách bằng dấu ","
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Chuyển chuỗi thành List<string> bằng cách tách chuỗi bằng dấu ","
                ); ;
            builder.HasOne<User>(post => post.User)
                .WithMany(user => user.ListPost)
                .HasForeignKey(post => post.UserId);

            builder.HasOne<Faculty>(post => post.Faculty)
                .WithMany(faculty => faculty.ListPost)
                .HasForeignKey(post => post.FacultyId);
        }
    }
}
