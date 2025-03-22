using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedValuesToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "notification_type",
                columns: new[] { "id", "name" },
                values: new object[] { 5, "Minute" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "notification_type",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
