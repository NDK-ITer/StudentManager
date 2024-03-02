using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAvatarPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2c75293b-f8e5-4862-9b13-5894a64895cd");

            migrationBuilder.DropColumn(
                name: "ListImage",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPost",
                table: "Post",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "76fda05c-b706-421c-94b7-a0a578c1b93b",
                column: "NormalizeName",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c8c17fbb-731c-405a-bed8-cb22868ef7ca",
                column: "NormalizeName",
                value: "Marketing Manager");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "da1ed655-5d7a-4d6d-ac03-540ceac69c91",
                column: "NormalizeName",
                value: "Marketing Coordinator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPost",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "ListImage",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "76fda05c-b706-421c-94b7-a0a578c1b93b",
                column: "NormalizeName",
                value: "User");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c8c17fbb-731c-405a-bed8-cb22868ef7ca",
                column: "NormalizeName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "da1ed655-5d7a-4d6d-ac03-540ceac69c91",
                column: "NormalizeName",
                value: "Manager");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Birthday", "CreatedDate", "FacultyID", "FirstEmail", "FirstName", "IsLock", "IsVerified", "LastName", "PasswordHash", "PhoneNumber", "PresentEmail", "RoleId", "TokenAccess", "UserName", "VerifiedDate" },
                values: new object[] { "2c75293b-f8e5-4862-9b13-5894a64895cd", "", new DateTime(2024, 2, 27, 18, 30, 18, 892, DateTimeKind.Local).AddTicks(5054), new DateTime(2024, 2, 27, 18, 30, 18, 892, DateTimeKind.Local).AddTicks(5069), null, "admin001@gmail.com", "Admin", false, true, "account", "OupciF/ZKYnt4U0xYizqoQ==", "0123456789", "admin001@gmail.com", "c8c17fbb-731c-405a-bed8-cb22868ef7ca", "3843EAE40C07AD7100F5410636E4CE885724694BBE462BD472372914E91DDC6EFA33B30E9A078F2106C9AD852103238DC52E9D3839D27F51ECE91CE932738227", "adminVersion_0001", new DateTime(2024, 2, 27, 18, 30, 18, 892, DateTimeKind.Local).AddTicks(5354) });
        }
    }
}
