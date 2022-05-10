using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class userProfileId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBooks");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "UserBooks",
                newName: "BookID");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_BookId",
                table: "UserBooks",
                newName: "IX_UserBooks_BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BookID",
                table: "UserBooks",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BookID",
                table: "UserBooks");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "UserBooks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_BookID",
                table: "UserBooks",
                newName: "IX_UserBooks_BookId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
