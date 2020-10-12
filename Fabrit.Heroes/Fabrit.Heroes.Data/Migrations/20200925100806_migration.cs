using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroBadge_Badges_BadgeId",
                table: "HeroBadge");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroBadge_Heroes_HeroId",
                table: "HeroBadge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroBadge",
                table: "HeroBadge");

            migrationBuilder.RenameTable(
                name: "HeroBadge",
                newName: "HeroBadges");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBadge_HeroId",
                table: "HeroBadges",
                newName: "IX_HeroBadges_HeroId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBadge_BadgeId",
                table: "HeroBadges",
                newName: "IX_HeroBadges_BadgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroBadges",
                table: "HeroBadges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBadges_Badges_BadgeId",
                table: "HeroBadges",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBadges_Heroes_HeroId",
                table: "HeroBadges",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroBadges_Badges_BadgeId",
                table: "HeroBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroBadges_Heroes_HeroId",
                table: "HeroBadges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroBadges",
                table: "HeroBadges");

            migrationBuilder.RenameTable(
                name: "HeroBadges",
                newName: "HeroBadge");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBadges_HeroId",
                table: "HeroBadge",
                newName: "IX_HeroBadge_HeroId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroBadges_BadgeId",
                table: "HeroBadge",
                newName: "IX_HeroBadge_BadgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroBadge",
                table: "HeroBadge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBadge_Badges_BadgeId",
                table: "HeroBadge",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroBadge_Heroes_HeroId",
                table: "HeroBadge",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
