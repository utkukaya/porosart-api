using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class remove_team_content_from_team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_team_content_team_content_id",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "ix_teams_team_content_id",
                table: "teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_teams_team_content_id",
                table: "teams",
                column: "team_content_id");

            migrationBuilder.AddForeignKey(
                name: "fk_teams_team_content_team_content_id",
                table: "teams",
                column: "team_content_id",
                principalTable: "team_content",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
