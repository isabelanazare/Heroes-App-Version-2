using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class addedHeroBadge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroBadge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BadgeId = table.Column<int>(nullable: false),
                    HeroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroBadge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroBadge_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroBadge_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroBadge_BadgeId",
                table: "HeroBadge",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroBadge_HeroId",
                table: "HeroBadge",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroBadge");
        }
    }
}
