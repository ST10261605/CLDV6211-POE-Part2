using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCKhumaloCraftFinal2.Migrations
{
    /// <inheritdoc />
    public partial class SixthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userID",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_userID",
                table: "CartItem",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_User_userID",
                table: "CartItem",
                column: "userID",
                principalTable: "User",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_User_userID",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_userID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "CartItem");
        }
    }
}
