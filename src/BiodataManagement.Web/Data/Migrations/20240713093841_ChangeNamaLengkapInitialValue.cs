using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiodataManagement.Data.Migrations
{
    public partial class ChangeNamaLengkapInitialValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "141970ff-9403-4737-8782-0776a32a9f3b", "37fd3477-0ad8-44ae-8339-74c785f2a998", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c65477f8-e200-41ec-a609-3db5a19e06b9", "e250c0ce-de93-4999-9b41-6c2c1562140a", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "141970ff-9403-4737-8782-0776a32a9f3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c65477f8-e200-41ec-a609-3db5a19e06b9");

            migrationBuilder.AlterColumn<string>(
                name: "NamaLengkap",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c11d8041-5d51-4034-a06a-6a63822c104a", "35f88dbd-5ce3-4b72-bfe9-e35ee88ab7b2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebc717ed-4a75-413a-b6ee-a8307eed695c", "a21b1245-31b2-484c-9321-a7bc75d300c3", "User", "USER" });
        }
    }
}
