using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartingSystemSimulation.Migrations
{
    /// <inheritdoc />
    public partial class FreshSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Karts",
                columns: table => new
                {
                    KartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KartType = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karts", x => x.KartId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    LoginEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LoginEmail);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceType = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    TopRacers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KartId = table.Column<int>(type: "int", nullable: false),
                    LiveRaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Karts_KartId",
                        column: x => x.KartId,
                        principalTable: "Karts",
                        principalColumn: "KartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserLoginEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "LoginEmail",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserLoginEmail",
                        column: x => x.UserLoginEmail,
                        principalTable: "Users",
                        principalColumn: "LoginEmail");
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    SupervisorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserLoginEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.SupervisorId);
                    table.ForeignKey(
                        name: "FK_Supervisors_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "LoginEmail",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supervisors_Users_UserLoginEmail",
                        column: x => x.UserLoginEmail,
                        principalTable: "Users",
                        principalColumn: "LoginEmail");
                });

            migrationBuilder.CreateTable(
                name: "LiveRaces",
                columns: table => new
                {
                    LiveRaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    RaceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveRaces", x => x.LiveRaceId);
                    table.ForeignKey(
                        name: "FK_LiveRaces_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Racers",
                columns: table => new
                {
                    RacerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<int>(type: "int", nullable: false),
                    AgreedToRules = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AssignedKartId = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    LiveRaceId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserLoginEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racers", x => x.RacerId);
                    table.ForeignKey(
                        name: "FK_Racers_Karts_AssignedKartId",
                        column: x => x.AssignedKartId,
                        principalTable: "Karts",
                        principalColumn: "KartId");
                    table.ForeignKey(
                        name: "FK_Racers_LiveRaces_LiveRaceId",
                        column: x => x.LiveRaceId,
                        principalTable: "LiveRaces",
                        principalColumn: "LiveRaceId");
                    table.ForeignKey(
                        name: "FK_Racers_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "SupervisorId");
                    table.ForeignKey(
                        name: "FK_Racers_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "LoginEmail",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racers_Users_UserLoginEmail",
                        column: x => x.UserLoginEmail,
                        principalTable: "Users",
                        principalColumn: "LoginEmail");
                });

            migrationBuilder.CreateTable(
                name: "Leaderboards",
                columns: table => new
                {
                    LeaderboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    BestTiming = table.Column<TimeSpan>(type: "time", nullable: false),
                    StarsEarned = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboards", x => x.LeaderboardId);
                    table.ForeignKey(
                        name: "FK_Leaderboards_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    MembershipType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FreeTickets = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.MembershipId);
                    table.ForeignKey(
                        name: "FK_Memberships_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    BookingType = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_RaceBookings_Games_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceBookings_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceHistories",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    BestTiming = table.Column<TimeSpan>(type: "time", nullable: false),
                    StarsEarned = table.Column<int>(type: "int", nullable: false),
                    RaceHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceHistories", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_RaceHistories_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceRacers",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    RacerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceRacers", x => new { x.RaceId, x.RacerId });
                    table.ForeignKey(
                        name: "FK_RaceRacers_Games_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceRacers_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorRacers",
                columns: table => new
                {
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    RacerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorRacers", x => new { x.SupervisorId, x.RacerId });
                    table.ForeignKey(
                        name: "FK_SupervisorRacers_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorRacers_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "SupervisorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceHistoryLeaderboards",
                columns: table => new
                {
                    RaceHistoryId = table.Column<int>(type: "int", nullable: false),
                    LeaderboardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceHistoryLeaderboards", x => new { x.RaceHistoryId, x.LeaderboardId });
                    table.ForeignKey(
                        name: "FK_RaceHistoryLeaderboards_Leaderboards_LeaderboardId",
                        column: x => x.LeaderboardId,
                        principalTable: "Leaderboards",
                        principalColumn: "LeaderboardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceHistoryLeaderboards_RaceHistories_RaceHistoryId",
                        column: x => x.RaceHistoryId,
                        principalTable: "RaceHistories",
                        principalColumn: "HistoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserLoginEmail",
                table: "Admins",
                column: "UserLoginEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Games_KartId",
                table: "Games",
                column: "KartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboards_RacerId",
                table: "Leaderboards",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveRaceRacers_LiveRaceId",
                table: "LiveRaceRacers",
                column: "LiveRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveRaceRacers_RacerId",
                table: "LiveRaceRacers",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveRaces_GameId",
                table: "LiveRaces",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_RacerId",
                table: "Memberships",
                column: "RacerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaceBookings_RaceId",
                table: "RaceBookings",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceBookings_RacerId",
                table: "RaceBookings",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistories_RacerId",
                table: "RaceHistories",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistoryLeaderboards_LeaderboardId",
                table: "RaceHistoryLeaderboards",
                column: "LeaderboardId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceRacers_RacerId",
                table: "RaceRacers",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_AssignedKartId",
                table: "Racers",
                column: "AssignedKartId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_Email",
                table: "Racers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Racers_LiveRaceId",
                table: "Racers",
                column: "LiveRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_SupervisorId",
                table: "Racers",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_UserLoginEmail",
                table: "Racers",
                column: "UserLoginEmail");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorRacers_RacerId",
                table: "SupervisorRacers",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_Email",
                table: "Supervisors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_UserLoginEmail",
                table: "Supervisors",
                column: "UserLoginEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "LiveRaceRacers");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "RaceBookings");

            migrationBuilder.DropTable(
                name: "RaceHistoryLeaderboards");

            migrationBuilder.DropTable(
                name: "RaceRacers");

            migrationBuilder.DropTable(
                name: "SupervisorRacers");

            migrationBuilder.DropTable(
                name: "Leaderboards");

            migrationBuilder.DropTable(
                name: "RaceHistories");

            migrationBuilder.DropTable(
                name: "Racers");

            migrationBuilder.DropTable(
                name: "LiveRaces");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Karts");
        }
    }
}
