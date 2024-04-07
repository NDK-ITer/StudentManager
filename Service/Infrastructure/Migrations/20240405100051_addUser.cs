using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "76fda05c-b706-421c-94b7-a0a578c1b93b",
                column: "Name",
                value: "STUDENT");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name", "NormalizeName" },
                values: new object[] { "af3125d7-6fde-4ea7-836c-32c0dc1ad030", "", "USER", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "af3125d7-6fde-4ea7-836c-32c0dc1ad030");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "76fda05c-b706-421c-94b7-a0a578c1b93b",
                column: "Name",
                value: "USER");
        }
    }
}
