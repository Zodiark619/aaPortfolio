using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class meong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TodoTasks",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedBy",
                table: "TodoTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskStatus",
                table: "TodoTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "TodoTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "TodoTasks");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "TodoTasks",
                newName: "UserId");
        }
    }
}
