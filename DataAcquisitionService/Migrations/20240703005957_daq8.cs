using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    ProcessType = table.Column<int>(type: "int", nullable: false),
                    InsertType = table.Column<int>(type: "int", nullable: false),
                    RowsTotal = table.Column<int>(type: "int", nullable: true),
                    RowsAdded = table.Column<int>(type: "int", nullable: true),
                    RowsUpdated = table.Column<int>(type: "int", nullable: true),
                    RowsDeleted = table.Column<int>(type: "int", nullable: true),
                    RowsWarning = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityID = table.Column<int>(type: "int", nullable: true),
                    Exchange = table.Column<int>(type: "int", nullable: true),
                    PriceRunID = table.Column<int>(type: "int", nullable: true),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LTP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradedVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Price_PriceRuns_PriceRunID",
                        column: x => x.PriceRunID,
                        principalTable: "PriceRuns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Price_PriceRunID",
                table: "Price",
                column: "PriceRunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "PriceRuns");
        }
    }
}
