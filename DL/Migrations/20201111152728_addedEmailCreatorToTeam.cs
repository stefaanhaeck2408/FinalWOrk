using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class addedEmailCreatorToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailCreator",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCreator",
                table: "Teams");
        }
    }
}
