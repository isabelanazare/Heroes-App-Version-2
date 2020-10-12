using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class modifiedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroBattle_Battle_BattleId",
                table: "HeroBattle");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroBattle_Heroes_HeroId",
                table: "HeroBattle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroBattle",
                table: "HeroBattle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Battle",
                table: "Battle");

            migrationBuilder.RenameTable(
                name: "HeroBattle",
                newName: "HeroBattles");

            migrationBuilder.RenameTable(
                name: "Battle",
                newName: "Battles");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBattle_HeroId",
                table: "HeroBattles",
                newName: "IX_HeroBattles_HeroId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBattle_BattleId",
                table: "HeroBattles",
                newName: "IX_HeroBattles_BattleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroBattles",
                table: "HeroBattles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battles",
                table: "Battles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBattles_Battles_BattleId",
                table: "HeroBattles",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBattles_Heroes_HeroId",
                table: "HeroBattles",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroBattles_Battles_BattleId",
                table: "HeroBattles");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroBattles_Heroes_HeroId",
                table: "HeroBattles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroBattles",
                table: "HeroBattles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Battles",
                table: "Battles");

            migrationBuilder.RenameTable(
                name: "HeroBattles",
                newName: "HeroBattle");

            migrationBuilder.RenameTable(
                name: "Battles",
                newName: "Battle");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBattles_HeroId",
                table: "HeroBattle",
                newName: "IX_HeroBattle_HeroId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBattles_BattleId",
                table: "HeroBattle",
                newName: "IX_HeroBattle_BattleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroBattle",
                table: "HeroBattle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battle",
                table: "Battle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBattle_Battle_BattleId",
                table: "HeroBattle",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBattle_Heroes_HeroId",
                table: "HeroBattle",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
