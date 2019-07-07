using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class buildingHouseManagers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingHousemanagers",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false),
                    HouseManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingHousemanagers", x => new { x.BuildingId, x.HouseManagerId });
                    table.ForeignKey(
                        name: "FK_BuildingHousemanagers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingHousemanagers_AppUsers_HouseManagerId",
                        column: x => x.HouseManagerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingHousemanagers_HouseManagerId",
                table: "BuildingHousemanagers",
                column: "HouseManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingHousemanagers");
        }
    }
}
