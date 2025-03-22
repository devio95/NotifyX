using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class IsProcessingColumn_changeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_processing",
                table: "notification_executions",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_processing",
                table: "notification_executions",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
