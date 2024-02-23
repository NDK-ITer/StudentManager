using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIsOpenFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ed781edb-bcb1-4f7e-a7c5-26acbe3cd389");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Faculty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateComment",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[,]
                {
                    { "46a7b851-722d-40a2-8ab2-a578583056b6", "", "ADMIN", "Admin" },
                    { "66c83f38-2377-413e-9135-854ae6dd57ea", "", "USER", "User" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd",
                columns: new[] { "Birthday", "CreatedDate", "RoleId", "TokenAccess", "VerifiedDate" },
                values: new object[] { new DateTime(2024, 2, 23, 8, 53, 9, 463, DateTimeKind.Local).AddTicks(7278), new DateTime(2024, 2, 23, 8, 53, 9, 463, DateTimeKind.Local).AddTicks(7291), "46a7b851-722d-40a2-8ab2-a578583056b6", "BACCE47D2A3A0EA99AB542334C3B963FF426C084ABD3E7CA5A3A01607A648AFDC5FF3EE1F0A53A887CD6B9736E2F06CBE5EC780AFD411F8DA9AFC10964A64CEE", new DateTime(2024, 2, 23, 8, 53, 9, 463, DateTimeKind.Local).AddTicks(7711) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "46a7b851-722d-40a2-8ab2-a578583056b6");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "66c83f38-2377-413e-9135-854ae6dd57ea");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "DateComment",
                table: "Comment");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[,]
                {
                    { "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb", "", "ADMIN", "Admin" },
                    { "ed781edb-bcb1-4f7e-a7c5-26acbe3cd389", "", "USER", "User" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd",
                columns: new[] { "Birthday", "CreatedDate", "RoleId", "TokenAccess", "VerifiedDate" },
                values: new object[] { new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2396), new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2409), "7c2e4a14-7a65-4a3b-8f46-fa110ef975eb", "83371688148C3EFE4436A96E373A24589762213BDBBC8B097403F663C9DFC7DC5DA73A801677579F8E204940BBA409D5717F8769911372CA6AA69ADED1ECFFEA", new DateTime(2024, 2, 22, 10, 51, 33, 708, DateTimeKind.Local).AddTicks(2691) });
        }
    }
}
