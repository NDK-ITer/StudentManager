using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "2ec60b31-4ff8-4ed1-99db-1ed978e546ea");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "a7b67950-d20f-4e99-a9f0-560835b20cff");

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[,]
                {
                    { "2ec60b31-4ff8-4ed1-99db-1ed978e546ea", "", "USER", "User" },
                    { "a7b67950-d20f-4e99-a9f0-560835b20cff", "", "ADMIN", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Birthday", "CreatedDate", "FirstEmail", "FirstName", "IsLock", "IsVerified", "LastName", "PasswordHash", "PhoneNumber", "PresentEmail", "RoleId", "TokenAccess", "UserName", "VerifiedDate" },
                values: new object[] { "2c75293b-f8e5-4862-9b13-5894a64895cd", "", new DateTime(2024, 2, 21, 12, 37, 22, 758, DateTimeKind.Local).AddTicks(6638), new DateTime(2024, 2, 21, 12, 37, 22, 758, DateTimeKind.Local).AddTicks(6652), "admin001@gmail.com", "Admin", false, true, "account", "OupciF/ZKYnt4U0xYizqoQ==", "0123456789", "admin001@gmail.com", "a7b67950-d20f-4e99-a9f0-560835b20cff", "68848E65B924C51EE2B5BDB51F1B9CA37D95410378B2D8BCF61F0AF3426B5EDBC1FD3204F8F680C1CF15821AA88421A4967BD9BC0A7423B7C66BF54A000EBBAB", "adminVersion_0001", new DateTime(2024, 2, 21, 12, 37, 22, 758, DateTimeKind.Local).AddTicks(6911) });
        }
    }
}
