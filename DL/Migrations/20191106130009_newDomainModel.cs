using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class newDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ronden",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ronden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeVragen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeVragen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizROndeTussentabellen",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false),
                    RondeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_QuizROndeTussentabellen_Quizen_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizROndeTussentabellen_Ronden_RondeId",
                        column: x => x.RondeId,
                        principalTable: "Ronden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizTeamTussentabellen",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_QuizTeamTussentabellen_Quizen_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizTeamTussentabellen_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vragen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VraagStelling = table.Column<string>(nullable: true),
                    MaxScoreVraag = table.Column<int>(nullable: false),
                    JsonCorrecteAntwoord = table.Column<string>(nullable: true),
                    JsonMogelijkeAntwoorden = table.Column<string>(nullable: true),
                    TypeVraagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vragen_TypeVragen_TypeVraagId",
                        column: x => x.TypeVraagId,
                        principalTable: "TypeVragen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngevoerdAntwoorden",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonAntwoord = table.Column<string>(nullable: true),
                    GescoordeScore = table.Column<int>(nullable: false),
                    VraagId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngevoerdAntwoorden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngevoerdAntwoorden_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngevoerdAntwoorden_Vragen_VraagId",
                        column: x => x.VraagId,
                        principalTable: "Vragen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RondeVraagTussentabellen",
                columns: table => new
                {
                    RondeId = table.Column<int>(nullable: false),
                    VraagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_RondeVraagTussentabellen_Ronden_RondeId",
                        column: x => x.RondeId,
                        principalTable: "Ronden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RondeVraagTussentabellen_Vragen_VraagId",
                        column: x => x.VraagId,
                        principalTable: "Vragen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngevoerdAntwoorden_TeamId",
                table: "IngevoerdAntwoorden",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_IngevoerdAntwoorden_VraagId",
                table: "IngevoerdAntwoorden",
                column: "VraagId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizROndeTussentabellen_QuizId",
                table: "QuizROndeTussentabellen",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizROndeTussentabellen_RondeId",
                table: "QuizROndeTussentabellen",
                column: "RondeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTeamTussentabellen_QuizId",
                table: "QuizTeamTussentabellen",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTeamTussentabellen_TeamId",
                table: "QuizTeamTussentabellen",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_RondeVraagTussentabellen_RondeId",
                table: "RondeVraagTussentabellen",
                column: "RondeId");

            migrationBuilder.CreateIndex(
                name: "IX_RondeVraagTussentabellen_VraagId",
                table: "RondeVraagTussentabellen",
                column: "VraagId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragen_TypeVraagId",
                table: "Vragen",
                column: "TypeVraagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngevoerdAntwoorden");

            migrationBuilder.DropTable(
                name: "QuizROndeTussentabellen");

            migrationBuilder.DropTable(
                name: "QuizTeamTussentabellen");

            migrationBuilder.DropTable(
                name: "RondeVraagTussentabellen");

            migrationBuilder.DropTable(
                name: "Quizen");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Ronden");

            migrationBuilder.DropTable(
                name: "Vragen");

            migrationBuilder.DropTable(
                name: "TypeVragen");
        }
    }
}
