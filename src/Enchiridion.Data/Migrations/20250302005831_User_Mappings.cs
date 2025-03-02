using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enchiridion.Data.Migrations
{
    /// <inheritdoc />
    public partial class User_Mappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_author_requests_app_users_user_id",
                table: "author_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_authors_app_users_user_id",
                table: "authors");

            migrationBuilder.DropForeignKey(
                name: "fk_habits_app_users_user_id",
                table: "habits");

            migrationBuilder.DropForeignKey(
                name: "fk_routines_app_users_user_id",
                table: "routines");

            migrationBuilder.DropForeignKey(
                name: "fk_todos_app_users_user_id",
                table: "todos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_app_users",
                table: "app_users");

            migrationBuilder.RenameTable(
                name: "app_users",
                newName: "users");

            migrationBuilder.AddColumn<string>(
                name: "auth_id",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_users_auth_id",
                table: "users",
                column: "auth_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_author_requests_users_user_id",
                table: "author_requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_habits_users_user_id",
                table: "habits",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routines_users_user_id",
                table: "routines",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_todos_users_user_id",
                table: "todos",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_author_requests_users_user_id",
                table: "author_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors");

            migrationBuilder.DropForeignKey(
                name: "fk_habits_users_user_id",
                table: "habits");

            migrationBuilder.DropForeignKey(
                name: "fk_routines_users_user_id",
                table: "routines");

            migrationBuilder.DropForeignKey(
                name: "fk_todos_users_user_id",
                table: "todos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_auth_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "auth_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "user_name",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "app_users");

            migrationBuilder.AddPrimaryKey(
                name: "pk_app_users",
                table: "app_users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_author_requests_app_users_user_id",
                table: "author_requests",
                column: "user_id",
                principalTable: "app_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_authors_app_users_user_id",
                table: "authors",
                column: "user_id",
                principalTable: "app_users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_habits_app_users_user_id",
                table: "habits",
                column: "user_id",
                principalTable: "app_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routines_app_users_user_id",
                table: "routines",
                column: "user_id",
                principalTable: "app_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_todos_app_users_user_id",
                table: "todos",
                column: "user_id",
                principalTable: "app_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
