using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class lastChangeTimeheroPower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastChangeTime",
                table: "HeroPowers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChangeTime",
                table: "HeroPowers");
        }
    }
}
