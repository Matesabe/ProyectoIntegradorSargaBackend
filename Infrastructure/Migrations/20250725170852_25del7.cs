using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _25del7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductPromotion",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchasePromotionProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromotion", x => new { x.ProductId, x.PurchasePromotionProductsId });
                    table.ForeignKey(
                        name: "FK_ProductPromotion_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPromotion_PurchasePromotionsProducts_PurchasePromotionProductsId",
                        column: x => x.PurchasePromotionProductsId,
                        principalTable: "PurchasePromotionsProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotion_PurchasePromotionProductsId",
                table: "ProductPromotion",
                column: "PurchasePromotionProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPromotion");

            migrationBuilder.AddColumn<int>(
                name: "PurchasePromotionProductsId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchasePromotionProductsId",
                table: "Products",
                column: "PurchasePromotionProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "Products",
                column: "PurchasePromotionProductsId",
                principalTable: "PurchasePromotionsProducts",
                principalColumn: "Id");
        }
    }
}
