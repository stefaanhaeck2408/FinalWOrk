using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class addedPinAndQuizIdToTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Teams");
        }
    }
}
