using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songbook_backend.Migrations
{
    public partial class SongModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextPL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChordsOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SongPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongPartNumber = table.Column<int>(type: "int", nullable: false),
                    LinePosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitlePl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tempo = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Copyright = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasedOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlPl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlDrive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReadyToUser = table.Column<bool>(type: "bit", nullable: false),
                    IsInUse = table.Column<bool>(type: "bit", nullable: false),
                    LastUsed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CounterOfUse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "SongGroups");

            migrationBuilder.DropTable(
                name: "SongParts");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "SongTypes");
        }
    }
}
