using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoteAPI.Migrations
{
    public partial class RemoveAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Botes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Botes",
                type: "INTEGER",
                nullable: true);
        }
    }
}
