using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _24del7_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_PurchasePromotionProductsId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "PurchasePromotionProductsId",
                table: "SubProducts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PurchasePromotionProductsId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_PurchasePromotionProductsId",
                table: "SubProducts",
                column: "PurchasePromotionProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "SubProducts",
                column: "PurchasePromotionProductsId",
                principalTable: "PurchasePromotionsProducts",
                principalColumn: "Id");
        }
    }
}
