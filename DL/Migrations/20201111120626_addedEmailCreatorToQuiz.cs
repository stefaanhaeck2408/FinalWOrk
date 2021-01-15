using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class addedEmailCreatorToQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailCreator",
                table: "Quizen",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCreator",
                table: "Quizen");
        }
    }
}
