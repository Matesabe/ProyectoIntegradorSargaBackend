using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _12del8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Redemptions_RedemptionId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_RedemptionId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "RedemptionId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Redemptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RedemptionId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Redemptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_RedemptionId",
                table: "SubProducts",
                column: "RedemptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Redemptions_RedemptionId",
                table: "SubProducts",
                column: "RedemptionId",
                principalTable: "Redemptions",
                principalColumn: "Id");
        }
    }
}
