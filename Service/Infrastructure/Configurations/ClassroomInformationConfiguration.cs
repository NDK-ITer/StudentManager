using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ClassroomInformationConfiguration : IEntityTypeConfiguration<ClassroomInformation>
    {
        public void Configure(EntityTypeBuilder<ClassroomInformation> builder)
        {
            builder.ToTable(nameof(ClassroomInformation));
            builder.HasKey(x => x.IdClassroom);
            builder.Property(x => x.IdClassroom).HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(50);
            builder.Property(x => x.Avatar).HasMaxLength(100);
            builder.Property(x => x.LinkAvatar).HasMaxLength(100);
        }
    }
}
