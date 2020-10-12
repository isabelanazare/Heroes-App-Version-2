using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class Added_IsBadGuy_Location_for_Hero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBadGuy",
                table: "Heroes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBadGuy",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Heroes");
        }
    }
}
