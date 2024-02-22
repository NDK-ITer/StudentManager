using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired();

            builder.HasOne<Post>(cmt => cmt.Post)
                .WithMany(post => post.ListComent)
                .HasForeignKey(cmt => cmt.PostId);
        }
    }
}
