using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestConsole.Migrations
{
    public partial class IvanovAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Birthday", "LastName", "Name", "Patronymic", "Rating" },
                values: new object[] { 99999, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иванов", "Иван", "Иванович", 100.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 99999);
        }
    }
}
