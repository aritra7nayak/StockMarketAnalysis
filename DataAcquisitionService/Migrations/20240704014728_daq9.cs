using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_PriceRuns_PriceRunID",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                table: "Price");

            migrationBuilder.RenameTable(
                name: "Price",
                newName: "Prices");

            migrationBuilder.RenameIndex(
                name: "IX_Price_PriceRunID",
                table: "Prices",
                newName: "IX_Prices_PriceRunID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prices",
                table: "Prices",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_SecurityID",
                table: "Prices",
                column: "SecurityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_PriceRuns_PriceRunID",
                table: "Prices",
                column: "PriceRunID",
                principalTable: "PriceRuns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Securities_SecurityID",
                table: "Prices",
                column: "SecurityID",
                principalTable: "Securities",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceRuns_PriceRunID",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Securities_SecurityID",
                table: "Prices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prices",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_SecurityID",
                table: "Prices");

            migrationBuilder.RenameTable(
                name: "Prices",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_PriceRunID",
                table: "Price",
                newName: "IX_Price_PriceRunID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                table: "Price",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_PriceRuns_PriceRunID",
                table: "Price",
                column: "PriceRunID",
                principalTable: "PriceRuns",
                principalColumn: "Id");
        }
    }
}
