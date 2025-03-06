using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enchiridion.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_User_Author_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "ix_authors_user_id",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "ix_author_requests_user_id",
                table: "author_requests");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "authors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "author_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "user_author_subscription",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_author_subscription", x => new { x.user_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_user_author_subscription_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_author_subscription_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authors_user_id",
                table: "authors",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_author_requests_user_id",
                table: "author_requests",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_author_subscription_author_id",
                table: "user_author_subscription",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors");

            migrationBuilder.DropTable(
                name: "user_author_subscription");

            migrationBuilder.DropIndex(
                name: "ix_authors_user_id",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "ix_author_requests_user_id",
                table: "author_requests");

            migrationBuilder.DropColumn(
                name: "status",
                table: "author_requests");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "authors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "ix_authors_user_id",
                table: "authors",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_author_requests_user_id",
                table: "author_requests",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_authors_users_user_id",
                table: "authors",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
