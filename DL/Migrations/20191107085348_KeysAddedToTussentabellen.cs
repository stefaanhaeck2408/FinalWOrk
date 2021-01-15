using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class KeysAddedToTussentabellen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RondeVraagTussentabellen",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizTeamTussentabellen",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizROndeTussentabellen",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RondeVraagTussentabellen",
                table: "RondeVraagTussentabellen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizTeamTussentabellen",
                table: "QuizTeamTussentabellen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizROndeTussentabellen",
                table: "QuizROndeTussentabellen",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RondeVraagTussentabellen",
                table: "RondeVraagTussentabellen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizTeamTussentabellen",
                table: "QuizTeamTussentabellen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizROndeTussentabellen",
                table: "QuizROndeTussentabellen");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RondeVraagTussentabellen");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizTeamTussentabellen");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizROndeTussentabellen");
        }
    }
}
