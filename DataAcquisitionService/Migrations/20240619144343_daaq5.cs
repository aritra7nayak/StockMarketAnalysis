using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daaq5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE dbo.TypeSecurities AS TABLE
(
    SYMBOL NVARCHAR(50),
    Name NVARCHAR(255),
    Series NVARCHAR(50),
    Date NVARCHAR(50),
    PaidValue INT,
    MarketLot INT,
    ISIN NVARCHAR(50),
    FaceValue INT
);



");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
