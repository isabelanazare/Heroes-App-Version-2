using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class addedlastTrainingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTrainingTime",
                table: "HeroPowers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "HeroPowers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTrainingTime",
                table: "HeroPowers");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "HeroPowers");
        }
    }
}
