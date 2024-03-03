using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserUpvotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Image",
                keyValue: null,
                column: "Image",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserUpvote",
                columns: table => new
                {
                    UpvoterId = table.Column<int>(type: "int", nullable: false),
                    UpvotedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUpvote", x => new { x.UpvoterId, x.UpvotedUserId });
                    table.ForeignKey(
                        name: "FK_UserUpvote_Users_UpvotedUserId",
                        column: x => x.UpvotedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserUpvote_Users_UpvoterId",
                        column: x => x.UpvoterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jokes_AuthorId",
                table: "Jokes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUpvote_UpvotedUserId",
                table: "UserUpvote",
                column: "UpvotedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jokes_Users_AuthorId",
                table: "Jokes",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jokes_Users_AuthorId",
                table: "Jokes");

            migrationBuilder.DropTable(
                name: "UserUpvote");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Jokes_AuthorId",
                table: "Jokes");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
