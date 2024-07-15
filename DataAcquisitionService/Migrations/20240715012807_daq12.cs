using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorporateActionRuns",
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
                    table.PrimaryKey("PK_CorporateActionRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorporateActionTypeRuns",
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
                    table.PrimaryKey("PK_CorporateActionTypeRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorporateActionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateActionTypeRunID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateActionTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorporateActionTypes_CorporateActionTypeRuns_CorporateActionTypeRunID",
                        column: x => x.CorporateActionTypeRunID,
                        principalTable: "CorporateActionTypeRuns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CorporateActions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityID = table.Column<int>(type: "int", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorporateActionTypeID = table.Column<int>(type: "int", nullable: true),
                    CorporateActionRunID = table.Column<int>(type: "int", nullable: true),
                    SecurityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Record_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Book_Closure_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Book_Closure_End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateActions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorporateActions_CorporateActionRuns_CorporateActionRunID",
                        column: x => x.CorporateActionRunID,
                        principalTable: "CorporateActionRuns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CorporateActions_CorporateActionTypes_CorporateActionTypeID",
                        column: x => x.CorporateActionTypeID,
                        principalTable: "CorporateActionTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorporateActions_Securities_SecurityID",
                        column: x => x.SecurityID,
                        principalTable: "Securities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorporateActions_CorporateActionRunID",
                table: "CorporateActions",
                column: "CorporateActionRunID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateActions_CorporateActionTypeID",
                table: "CorporateActions",
                column: "CorporateActionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateActions_SecurityID",
                table: "CorporateActions",
                column: "SecurityID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateActionTypes_CorporateActionTypeRunID",
                table: "CorporateActionTypes",
                column: "CorporateActionTypeRunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorporateActions");

            migrationBuilder.DropTable(
                name: "CorporateActionRuns");

            migrationBuilder.DropTable(
                name: "CorporateActionTypes");

            migrationBuilder.DropTable(
                name: "CorporateActionTypeRuns");
        }
    }
}
