using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserUpvotes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUpvote_Users_UpvotedUserId",
                table: "UserUpvote");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUpvote_Users_UpvoterId",
                table: "UserUpvote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUpvote",
                table: "UserUpvote");

            migrationBuilder.RenameTable(
                name: "UserUpvote",
                newName: "UsersUpvote");

            migrationBuilder.RenameIndex(
                name: "IX_UserUpvote_UpvotedUserId",
                table: "UsersUpvote",
                newName: "IX_UsersUpvote_UpvotedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersUpvote",
                table: "UsersUpvote",
                columns: new[] { "UpvoterId", "UpvotedUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersUpvote_Users_UpvotedUserId",
                table: "UsersUpvote",
                column: "UpvotedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersUpvote_Users_UpvoterId",
                table: "UsersUpvote",
                column: "UpvoterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersUpvote_Users_UpvotedUserId",
                table: "UsersUpvote");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersUpvote_Users_UpvoterId",
                table: "UsersUpvote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersUpvote",
                table: "UsersUpvote");

            migrationBuilder.RenameTable(
                name: "UsersUpvote",
                newName: "UserUpvote");

            migrationBuilder.RenameIndex(
                name: "IX_UsersUpvote_UpvotedUserId",
                table: "UserUpvote",
                newName: "IX_UserUpvote_UpvotedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUpvote",
                table: "UserUpvote",
                columns: new[] { "UpvoterId", "UpvotedUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUpvote_Users_UpvotedUserId",
                table: "UserUpvote",
                column: "UpvotedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUpvote_Users_UpvoterId",
                table: "UserUpvote",
                column: "UpvoterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
