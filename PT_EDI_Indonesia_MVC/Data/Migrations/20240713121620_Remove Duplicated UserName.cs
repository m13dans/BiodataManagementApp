using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiodataManagement.Data.Migrations
{
    public partial class RemoveDuplicatedUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "141970ff-9403-4737-8782-0776a32a9f3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c65477f8-e200-41ec-a609-3db5a19e06b9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "655a13af-381d-4108-99f4-c4a8c3867d0b", "c2629e8d-965d-4f0b-b9f4-7ee41d6fda50", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7fb7b60f-b440-4dcc-99bb-6bff0900fe5f", "bba70b90-dc2c-432b-bfb9-84fb06373f3f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "655a13af-381d-4108-99f4-c4a8c3867d0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fb7b60f-b440-4dcc-99bb-6bff0900fe5f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "141970ff-9403-4737-8782-0776a32a9f3b", "37fd3477-0ad8-44ae-8339-74c785f2a998", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c65477f8-e200-41ec-a609-3db5a19e06b9", "e250c0ce-de93-4999-9b41-6c2c1562140a", "Admin", "ADMIN" });
        }
    }
}
