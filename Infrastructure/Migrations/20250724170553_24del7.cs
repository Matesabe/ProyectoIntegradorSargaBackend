using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _24del7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Reports_Reportid",
                table: "Entry");

            migrationBuilder.RenameColumn(
                name: "Reportid",
                table: "Entry",
                newName: "ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_Reportid",
                table: "Entry",
                newName: "IX_Entry_ReportId");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Entry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Reports_ReportId",
                table: "Entry",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Reports_ReportId",
                table: "Entry");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Entry",
                newName: "Reportid");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_ReportId",
                table: "Entry",
                newName: "IX_Entry_Reportid");

            migrationBuilder.AlterColumn<int>(
                name: "Reportid",
                table: "Entry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Reports_Reportid",
                table: "Entry",
                column: "Reportid",
                principalTable: "Reports",
                principalColumn: "id");
        }
    }
}
