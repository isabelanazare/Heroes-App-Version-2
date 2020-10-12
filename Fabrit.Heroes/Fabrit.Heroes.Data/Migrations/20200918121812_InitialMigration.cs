using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrit.Heroes.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    WasPasswordForgotten = table.Column<bool>(nullable: false),
                    WasPasswordChanged = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Strength = table.Column<int>(nullable: false),
                    ElementId = table.Column<int>(nullable: false),
                    MainTrait = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Powers_Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_HeroTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "HeroTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroAllies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllyToId = table.Column<int>(nullable: false),
                    AllyFromId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroAllies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroAllies_Heroes_AllyFromId",
                        column: x => x.AllyFromId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroAllies_Heroes_AllyToId",
                        column: x => x.AllyToId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HeroPowers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroId = table.Column<int>(nullable: false),
                    PowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroPowers_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroPowers_Powers_PowerId",
                        column: x => x.PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroAllies_AllyFromId",
                table: "HeroAllies",
                column: "AllyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAllies_AllyToId",
                table: "HeroAllies",
                column: "AllyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_Name",
                table: "Heroes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_TypeId",
                table: "Heroes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroPowers_HeroId",
                table: "HeroPowers",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroPowers_PowerId",
                table: "HeroPowers",
                column: "PowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_ElementId",
                table: "Powers",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true,
                filter: "[Token] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroAllies");

            migrationBuilder.DropTable(
                name: "HeroPowers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "HeroTypes");

            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
