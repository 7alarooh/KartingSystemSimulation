using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartingSystemSimulation.Migrations
{
    /// <inheritdoc />
    public partial class AddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racers_LiveRaces_LiveRaceId",
                table: "Racers");

            migrationBuilder.DropColumn(
                name: "CurrentLap",
                table: "LiveRaces");

            migrationBuilder.DropColumn(
                name: "LapTime",
                table: "LiveRaces");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "LiveRaces");

            migrationBuilder.CreateTable(
                name: "LiveRaceRacers",
                columns: table => new
                {
                    LiveRaceRacerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LiveRaceId = table.Column<int>(type: "int", nullable: false),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    CurrentLap = table.Column<int>(type: "int", nullable: false),
                    LapTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveRaceRacers", x => x.LiveRaceRacerId);
                    table.ForeignKey(
                        name: "FK_LiveRaceRacers_LiveRaces_LiveRaceId",
                        column: x => x.LiveRaceId,
                        principalTable: "LiveRaces",
                        principalColumn: "LiveRaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveRaceRacers_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveRaceRacers_LiveRaceId",
                table: "LiveRaceRacers",
                column: "LiveRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveRaceRacers_RacerId",
                table: "LiveRaceRacers",
                column: "RacerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Racers_LiveRaces_LiveRaceId",
                table: "Racers",
                column: "LiveRaceId",
                principalTable: "LiveRaces",
                principalColumn: "LiveRaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racers_LiveRaces_LiveRaceId",
                table: "Racers");

            migrationBuilder.DropTable(
                name: "LiveRaceRacers");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLap",
                table: "LiveRaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LapTime",
                table: "LiveRaces",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalTime",
                table: "LiveRaces",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_Racers_LiveRaces_LiveRaceId",
                table: "Racers",
                column: "LiveRaceId",
                principalTable: "LiveRaces",
                principalColumn: "LiveRaceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
