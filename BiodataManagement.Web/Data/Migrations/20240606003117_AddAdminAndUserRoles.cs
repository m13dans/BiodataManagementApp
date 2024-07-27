using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiodataManagement.Data.Migrations
{
    public partial class AddAdminAndUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NamaLengkap",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c11d8041-5d51-4034-a06a-6a63822c104a", "35f88dbd-5ce3-4b72-bfe9-e35ee88ab7b2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebc717ed-4a75-413a-b6ee-a8307eed695c", "a21b1245-31b2-484c-9321-a7bc75d300c3", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c11d8041-5d51-4034-a06a-6a63822c104a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebc717ed-4a75-413a-b6ee-a8307eed695c");

            migrationBuilder.AlterColumn<string>(
                name: "NamaLengkap",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
