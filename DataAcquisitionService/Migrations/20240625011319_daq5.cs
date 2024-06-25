using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daq5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BseCode",
                table: "Securities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BseCode",
                table: "Securities");
        }
    }
}
