using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class remove_team_id_from_team_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "team_id",
                table: "team_content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "team_id",
                table: "team_content",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
