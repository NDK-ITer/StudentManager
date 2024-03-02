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
            builder.Property(x => x.AvatarPost).HasMaxLength(100);
            

            builder.HasOne(post => post.Faculty)
                .WithMany(faculty => faculty.ListPost)
                .HasForeignKey(post => post.FacultyId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(post => post.User)
                .WithMany(User => User.ListPost)
                .HasForeignKey(post => post.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
