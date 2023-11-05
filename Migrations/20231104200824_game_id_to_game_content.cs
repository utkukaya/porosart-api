using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class game_id_to_game_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "game_id",
                table: "game_content",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "game_id",
                table: "game_content");
        }
    }
}
