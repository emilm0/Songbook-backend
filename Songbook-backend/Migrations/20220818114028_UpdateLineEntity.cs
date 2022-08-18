using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songbook_backend.Migrations
{
    public partial class UpdateLineEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextPL",
                table: "Lines",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Lines",
                newName: "TextPL");
        }
    }
}
