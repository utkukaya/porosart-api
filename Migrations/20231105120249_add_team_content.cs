using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace porosartapi.Migrations
{
    public partial class add_team_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "teams");

            migrationBuilder.AddColumn<int>(
                name: "team_content_id",
                table: "teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "team_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    html_code = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_content", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_team_content_team_content_id",
                table: "teams");

            migrationBuilder.DropTable(
                name: "team_content");

            migrationBuilder.DropIndex(
                name: "ix_teams_team_content_id",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "team_content_id",
                table: "teams");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "teams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
