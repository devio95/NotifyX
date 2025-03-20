using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class Nazwy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_notification_execution_next_notification_execu",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_notification_method_notification_method_id",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_notification_type_notification_type_id",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_users_user_id",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_execution_notification_notification_id",
                table: "notification_execution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_execution",
                table: "notification_execution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification",
                table: "notification");

            migrationBuilder.RenameTable(
                name: "notification_execution",
                newName: "notification_executions");

            migrationBuilder.RenameTable(
                name: "notification",
                newName: "notifications");

            migrationBuilder.RenameIndex(
                name: "ix_notification_execution_notification_id",
                table: "notification_executions",
                newName: "ix_notification_executions_notification_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_user_id",
                table: "notifications",
                newName: "ix_notifications_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_notification_type_id",
                table: "notifications",
                newName: "ix_notifications_notification_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_notification_method_id",
                table: "notifications",
                newName: "ix_notifications_notification_method_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_next_notification_execution_id",
                table: "notifications",
                newName: "ix_notifications_next_notification_execution_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_executions",
                table: "notification_executions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notifications",
                table: "notifications",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_executions_notifications_notification_id",
                table: "notification_executions",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_notification_executions_next_notification_exe",
                table: "notifications",
                column: "next_notification_execution_id",
                principalTable: "notification_executions",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_notification_method_notification_method_id",
                table: "notifications",
                column: "notification_method_id",
                principalTable: "notification_method",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_notification_type_notification_type_id",
                table: "notifications",
                column: "notification_type_id",
                principalTable: "notification_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_executions_notifications_notification_id",
                table: "notification_executions");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_notification_executions_next_notification_exe",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_notification_method_notification_method_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_notification_type_notification_type_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_users_user_id",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notifications",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_executions",
                table: "notification_executions");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "notification");

            migrationBuilder.RenameTable(
                name: "notification_executions",
                newName: "notification_execution");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_user_id",
                table: "notification",
                newName: "ix_notification_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_notification_type_id",
                table: "notification",
                newName: "ix_notification_notification_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_notification_method_id",
                table: "notification",
                newName: "ix_notification_notification_method_id");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_next_notification_execution_id",
                table: "notification",
                newName: "ix_notification_next_notification_execution_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_executions_notification_id",
                table: "notification_execution",
                newName: "ix_notification_execution_notification_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification",
                table: "notification",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_execution",
                table: "notification_execution",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_notification_execution_next_notification_execu",
                table: "notification",
                column: "next_notification_execution_id",
                principalTable: "notification_execution",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_notification_method_notification_method_id",
                table: "notification",
                column: "notification_method_id",
                principalTable: "notification_method",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_notification_type_notification_type_id",
                table: "notification",
                column: "notification_type_id",
                principalTable: "notification_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_users_user_id",
                table: "notification",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_execution_notification_notification_id",
                table: "notification_execution",
                column: "notification_id",
                principalTable: "notification",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
