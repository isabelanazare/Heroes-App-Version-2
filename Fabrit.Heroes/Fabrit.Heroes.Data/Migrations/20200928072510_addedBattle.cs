using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class addedBattle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitiatorId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroBattle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroId = table.Column<int>(nullable: false),
                    BattleId = table.Column<int>(nullable: false),
                    HasWon = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroBattle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroBattle_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroBattle_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroBattle_BattleId",
                table: "HeroBattle",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroBattle_HeroId",
                table: "HeroBattle",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroBattle");

            migrationBuilder.DropTable(
                name: "Battle");
        }
    }
}
