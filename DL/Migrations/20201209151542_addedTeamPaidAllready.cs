using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class addedTeamPaidAllready : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TeamPaidAllready",
                table: "Teams",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamPaidAllready",
                table: "Teams");
        }
    }
}
