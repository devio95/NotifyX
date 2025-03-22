using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class IsProcessingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_processing",
                table: "notification_executions",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_processing",
                table: "notification_executions");
        }
    }
}
