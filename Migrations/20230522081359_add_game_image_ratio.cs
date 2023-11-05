using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class add_game_image_ratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_ratio",
                table: "games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_ratio",
                table: "games");
        }
    }
}
