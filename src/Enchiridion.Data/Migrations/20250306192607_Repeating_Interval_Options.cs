using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enchiridion.Data.Migrations
{
    /// <inheritdoc />
    public partial class Repeating_Interval_Options : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_repeated",
                table: "todos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "todo_options_repeat_interval",
                table: "todos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "todo_options_start_date",
                table: "todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "habit_options_repeat_interval",
                table: "habits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "habit_options_start_date",
                table: "habits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_repeated",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "todo_options_repeat_interval",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "todo_options_start_date",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "habit_options_repeat_interval",
                table: "habits");

            migrationBuilder.DropColumn(
                name: "habit_options_start_date",
                table: "habits");
        }
    }
}
