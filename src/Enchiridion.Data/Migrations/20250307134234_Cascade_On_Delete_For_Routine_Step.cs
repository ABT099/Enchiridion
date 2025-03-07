using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enchiridion.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cascade_On_Delete_For_Routine_Step : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "todo_options_start_date",
                table: "todos");

            migrationBuilder.RenameColumn(
                name: "habit_options_start_date",
                table: "habits",
                newName: "habit_options_target_date");

            migrationBuilder.AlterColumn<int>(
                name: "todo_options_repeat_interval",
                table: "todos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "todo_options_target_date",
                table: "todos",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "todo_options_target_date",
                table: "todos");

            migrationBuilder.RenameColumn(
                name: "habit_options_target_date",
                table: "habits",
                newName: "habit_options_start_date");

            migrationBuilder.AlterColumn<int>(
                name: "todo_options_repeat_interval",
                table: "todos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "todo_options_start_date",
                table: "todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
