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
            var StudentId = "76fda05c-b706-421c-94b7-a0a578c1b93b";
            var StaffId = "8b657a23-9383-4fc5-aecc-6fa719143be1";
            var UserId = "af3125d7-6fde-4ea7-836c-32c0dc1ad030";
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
                    Id = StudentId,
                    Name = "STUDENT",
                    NormalizeName = "Student",
                    Description = ""
                },
                new Role()
                {
                    Id = ManagerId,
                    Name = "MANAGER",
                    NormalizeName = "Marketing Coordinator",
                    Description = ""
                },
                new Role()
                {
                    Id = StaffId,
                    Name = "STAFF",
                    NormalizeName = "Staff",
                    Description = ""
                },
                new Role()
                {
                    Id = UserId,
                    Name = "USER",
                    NormalizeName = "User",
                    Description = ""
                }
            );
        }
    }
}
