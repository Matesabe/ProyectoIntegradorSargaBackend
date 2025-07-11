using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asd8del7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Purchases_PurchaseId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_PurchaseId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "SubProducts");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseId",
                table: "Products",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchaseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_PurchaseId",
                table: "SubProducts",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Purchases_PurchaseId",
                table: "SubProducts",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
