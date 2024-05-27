using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCKhumaloCraftFinal2.Migrations
{
    /// <inheritdoc />
    public partial class FourthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_productID",
                table: "Order",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userID",
                table: "Order",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Product_productID",
                table: "Order",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_userID",
                table: "Order",
                column: "userID",
                principalTable: "User",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Product_productID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_userID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_productID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_userID",
                table: "Order");
        }
    }
}
