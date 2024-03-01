using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var AdminID = "c8c17fbb-731c-405a-bed8-cb22868ef7ca";
            var ManagerId = "da1ed655-5d7a-4d6d-ac03-540ceac69c91";
            var UserId = "76fda05c-b706-421c-94b7-a0a578c1b93b";
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = AdminID,
                    Name = "ADMIN",
                    NormalizeName = "Marketing Manager",
                    Description = ""
                },
                new Role()
                {
                    Id = UserId,
                    Name = "USER",
                    NormalizeName = "Student",
                    Description = ""
                },
                new Role()
                {
                    Id = ManagerId,
                    Name = "MANAGER",
                    NormalizeName = "Marketing Coordinator",
                    Description = ""
                }
            );
        }
    }
}
