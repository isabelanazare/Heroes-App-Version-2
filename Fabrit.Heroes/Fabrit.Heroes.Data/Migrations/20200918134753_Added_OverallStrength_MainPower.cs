using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class Added_OverallStrength_MainPower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeroId1",
                table: "HeroPowers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OverallStrength",
                table: "Heroes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HeroPowers_HeroId1",
                table: "HeroPowers",
                column: "HeroId1",
                unique: true,
                filter: "[HeroId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroPowers_Heroes_HeroId1",
                table: "HeroPowers",
                column: "HeroId1",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroPowers_Heroes_HeroId1",
                table: "HeroPowers");

            migrationBuilder.DropIndex(
                name: "IX_HeroPowers_HeroId1",
                table: "HeroPowers");

            migrationBuilder.DropColumn(
                name: "HeroId1",
                table: "HeroPowers");

            migrationBuilder.DropColumn(
                name: "OverallStrength",
                table: "Heroes");
        }
    }
}
