using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[,]
                {
                    { "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb", "", "ADMIN", "Admin" },
                    { "ed781edb-bcb1-4f7e-a7c5-26acbe3cd389", "", "USER", "User" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Birthday", "CreatedDate", "FacultyID", "FirstEmail", "FirstName", "IsLock", "IsVerified", "LastName", "PasswordHash", "PhoneNumber", "PresentEmail", "RoleId", "TokenAccess", "UserName", "VerifiedDate" },
                values: new object[] { "2c75293b-f8e5-4862-9b13-5894a64895cd", "", new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2396), new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2409), null, "admin001@gmail.com", "Admin", false, true, "account", "OupciF/ZKYnt4U0xYizqoQ==", "0123456789", "admin001@gmail.com", "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb", "83371688148C3EFE4436A96E373A24589762213BDBBC8B097403F663C9DFC7DC5DA73A801677579F8E204940BBA409D5717F8769911372CA6AA69ADED1ECFFEA", "adminVersion_0001", new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2691) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ed781edb-bcb1-4f7e-a7c5-26acbe3cd389");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Post");
        }
    }
}
