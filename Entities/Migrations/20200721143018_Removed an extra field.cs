using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Removedanextrafield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FisrtName",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FisrtName",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
