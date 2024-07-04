using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrevClose",
                table: "Prices",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevClose",
                table: "Prices");
        }
    }
}
