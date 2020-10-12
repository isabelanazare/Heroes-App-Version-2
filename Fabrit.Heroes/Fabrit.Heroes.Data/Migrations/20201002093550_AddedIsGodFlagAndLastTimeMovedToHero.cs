using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class AddedIsGodFlagAndLastTimeMovedToHero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGod",
                table: "Heroes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeMoved",
                table: "Heroes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGod",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "LastTimeMoved",
                table: "Heroes");
        }
    }
}
