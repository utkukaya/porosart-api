using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class add_team_id_to_team_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "team_content_id",
                table: "teams");

            migrationBuilder.AddColumn<int>(
                name: "team_id",
                table: "team_content",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "team_id",
                table: "team_content");

            migrationBuilder.AddColumn<int>(
                name: "team_content_id",
                table: "teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
