using Microsoft.EntityFrameworkCore.Migrations;

namespace FileTrackingSystem.DAL.Migrations.ApplicationDb
{
    public partial class DataFieldUpdateCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Company");
        }
    }
}
