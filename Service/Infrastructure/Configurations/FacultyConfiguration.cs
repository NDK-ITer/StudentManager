﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable(nameof(Faculty));
            builder.HasKey(x => x.Id);

            builder.HasOne<User>(faculty => faculty.Admin)
                .WithOne(user => user.Faculty)
                .HasForeignKey<Faculty>(fk => fk.AdminId);
        }
    }
}
