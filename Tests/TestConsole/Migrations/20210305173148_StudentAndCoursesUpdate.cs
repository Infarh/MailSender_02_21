using Microsoft.EntityFrameworkCore.Migrations;

namespace TestConsole.Migrations
{
    public partial class StudentAndCoursesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");
        }
    }
}
