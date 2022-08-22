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
                table: "UpdatedLines",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "UpdatedLines",
                newName: "TextPL");
        }
    }
}
