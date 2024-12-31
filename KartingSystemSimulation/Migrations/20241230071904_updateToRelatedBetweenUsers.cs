using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartingSystemSimulation.Migrations
{
    /// <inheritdoc />
    public partial class updateToRelatedBetweenUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserLoginEmail",
                table: "Supervisors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLoginEmail",
                table: "Racers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLoginEmail",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_UserLoginEmail",
                table: "Supervisors",
                column: "UserLoginEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_UserLoginEmail",
                table: "Racers",
                column: "UserLoginEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserLoginEmail",
                table: "Admins",
                column: "UserLoginEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_UserLoginEmail",
                table: "Admins",
                column: "UserLoginEmail",
                principalTable: "Users",
                principalColumn: "LoginEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Racers_Users_UserLoginEmail",
                table: "Racers",
                column: "UserLoginEmail",
                principalTable: "Users",
                principalColumn: "LoginEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_Users_UserLoginEmail",
                table: "Supervisors",
                column: "UserLoginEmail",
                principalTable: "Users",
                principalColumn: "LoginEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_UserLoginEmail",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Racers_Users_UserLoginEmail",
                table: "Racers");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_Users_UserLoginEmail",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisors_UserLoginEmail",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Racers_UserLoginEmail",
                table: "Racers");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserLoginEmail",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UserLoginEmail",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "UserLoginEmail",
                table: "Racers");

            migrationBuilder.DropColumn(
                name: "UserLoginEmail",
                table: "Admins");
        }
    }
}
