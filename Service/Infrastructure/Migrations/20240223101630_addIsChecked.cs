using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIsChecked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "46a7b851-722d-40a2-8ab2-a578583056b6");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "66c83f38-2377-413e-9135-854ae6dd57ea");

            migrationBuilder.RenameColumn(
                name: "isApproved",
                table: "Post",
                newName: "IsApproved");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Post",
                newName: "LinkDocument");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[,]
                {
                    { "76fda05c-b706-421c-94b7-a0a578c1b93b", "", "USER", "User" },
                    { "c8c17fbb-731c-405a-bed8-cb22868ef7ca", "", "ADMIN", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd",
                columns: new[] { "Birthday", "CreatedDate", "RoleId", "TokenAccess", "VerifiedDate" },
                values: new object[] { new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1107), new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1117), "c8c17fbb-731c-405a-bed8-cb22868ef7ca", "C09D185F3313EE93A4FA91598A0F124973C4AE6B7654C179FF13833D70FC5FF0B060F5A9FFAD733B86941B169F62880E6DB42B9C4F4F07C2958CA74BF4F373F7", new DateTime(2024, 2, 23, 17, 16, 28, 887, DateTimeKind.Local).AddTicks(1353) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "76fda05c-b706-421c-94b7-a0a578c1b93b");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c8c17fbb-731c-405a-bed8-cb22868ef7ca");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Post",
                newName: "isApproved");

            migrationBuilder.RenameColumn(
                name: "LinkDocument",
                table: "Post",
                newName: "Content");

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
    }
}
