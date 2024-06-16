using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecurityRunID",
                table: "Securities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecurityRun",
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
                    RowsAdded = table.Column<int>(type: "int", nullable: true),
                    RowsUpdated = table.Column<int>(type: "int", nullable: true),
                    RowsDeleted = table.Column<int>(type: "int", nullable: true),
                    RowsWarning = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRun", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Securities_SecurityRunID",
                table: "Securities",
                column: "SecurityRunID");

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_SecurityRun_SecurityRunID",
                table: "Securities",
                column: "SecurityRunID",
                principalTable: "SecurityRun",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Securities_SecurityRun_SecurityRunID",
                table: "Securities");

            migrationBuilder.DropTable(
                name: "SecurityRun");

            migrationBuilder.DropIndex(
                name: "IX_Securities_SecurityRunID",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "SecurityRunID",
                table: "Securities");
        }
    }
}
