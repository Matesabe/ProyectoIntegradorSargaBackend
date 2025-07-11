using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asd4del7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Warehouses_WarehouseId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_WarehouseId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "SubProducts");

            migrationBuilder.CreateTable(
                name: "WarehouseStock",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStock", x => new { x.WarehouseId, x.SubProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseStock_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseStock_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStock_SubProductId",
                table: "WarehouseStock",
                column: "SubProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseStock");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_WarehouseId",
                table: "SubProducts",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Warehouses_WarehouseId",
                table: "SubProducts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
