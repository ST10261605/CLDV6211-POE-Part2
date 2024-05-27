using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCKhumaloCraftFinal2.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    checkoutID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.checkoutID);
                    table.ForeignKey(
                        name: "FK_Checkout_CartItem_CartItemID",
                        column: x => x.CartItemID,
                        principalTable: "CartItem",
                        principalColumn: "CartItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_CartItemID",
                table: "Checkout",
                column: "CartItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkout");
        }
    }
}
