using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asd2del7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Products_SubProductId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Redemptions_RedemptionId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchaseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_RedemptionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WarehouseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProBrand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProGenre",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchasePromotionProductsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RedemptionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "SubProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    PurchasePromotionProductsId = table.Column<int>(type: "int", nullable: true),
                    RedemptionId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProducts_PurchasePromotionsProducts_PurchasePromotionProductsId",
                        column: x => x.PurchasePromotionProductsId,
                        principalTable: "PurchasePromotionsProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_Redemptions_RedemptionId",
                        column: x => x.RedemptionId,
                        principalTable: "Redemptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_PurchaseId",
                table: "SubProducts",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_PurchasePromotionProductsId",
                table: "SubProducts",
                column: "PurchasePromotionProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_RedemptionId",
                table: "SubProducts",
                column: "RedemptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_WarehouseId",
                table: "SubProducts",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_SubProducts_SubProductId",
                table: "Image",
                column: "SubProductId",
                principalTable: "SubProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_SubProducts_SubProductId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProBrand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProGenre",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasePromotionProductsId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RedemptionId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseId",
                table: "Products",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchasePromotionProductsId",
                table: "Products",
                column: "PurchasePromotionProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_RedemptionId",
                table: "Products",
                column: "RedemptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Products_SubProductId",
                table: "Image",
                column: "SubProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PurchasePromotionsProducts_PurchasePromotionProductsId",
                table: "Products",
                column: "PurchasePromotionProductsId",
                principalTable: "PurchasePromotionsProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Redemptions_RedemptionId",
                table: "Products",
                column: "RedemptionId",
                principalTable: "Redemptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
