using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var AdminID = Guid.NewGuid().ToString();
            var UserId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = AdminID,
                    Name = "ADMIN",
                    NormalizeName = "Admin",
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
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = "2c75293b-f8e5-4862-9b13-5894a64895cd",
                    UserName = "adminVersion_0001",
                    FirstEmail = "admin001@gmail.com",
                    PresentEmail = "admin001@gmail.com",
                    FirstName = "Admin",
                    LastName = "account",
                    IsLock = false,
                    Birthday = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    RoleId = AdminID,
                    TokenAccess = SecurityMethods.CreateRandomToken(),
                    VerifiedDate = DateTime.Now,
                    IsVerified = true,
                    PhoneNumber = "0123456789",
                    Avatar = string.Empty,
                    PasswordHash = SecurityMethods.HashPassword("AdminAccount123456789"),
                }
            );
        }
    }
}
