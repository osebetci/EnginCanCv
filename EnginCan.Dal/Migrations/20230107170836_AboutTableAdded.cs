using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnginCan.Dal.Migrations
{
    public partial class AboutTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    DogumTarih = table.Column<string>(nullable: true),
                    MezuniyetDurum = table.Column<string>(nullable: true),
                    DeneyimSuresi = table.Column<short>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    OzetMetin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");
        }
    }
}
