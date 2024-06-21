using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcquisitionService.Migrations
{
    /// <inheritdoc />
    public partial class daaq6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.spSecurityImporter
    @TypSecuritiesTable dbo.TypeSecurities READONLY,
    @RunDate DATE,
    @RunId INT,
    @RowsAdded INT OUTPUT,
    @TotalRows INT OUTPUT,
    @RowsUpdated INT OUTPUT,
    @RowsDeleted INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Initialize output parameters
    SET @RowsAdded = 0;
    SET @TotalRows = 0;
    SET @RowsUpdated = 0;
    SET @RowsDeleted = 0;



	    -- Delete rows not in the table type
    DELETE s
    FROM dbo.Securities AS s
    WHERE NOT EXISTS (
        SELECT 1
        FROM @TypSecuritiesTable AS ts
        WHERE s.SYMBOL = ts.SYMBOL
    );

    SET @RowsDeleted = @@ROWCOUNT;

    -- Update existing rows
    UPDATE s
    SET s.Name = ts.Name,
        s.Series = ts.Series,
        s.ListingDate = ts.Date,
        s.MarketLot = ts.MarketLot
    FROM dbo.Securities AS s
    INNER JOIN @TypSecuritiesTable AS ts
        ON s.SYMBOL = ts.SYMBOL;

    SET @RowsUpdated = @@ROWCOUNT;

	    -- Insert new rows
    INSERT INTO dbo.Securities (SYMBOL, Name, Series, ListingDate,  MarketLot)
    SELECT SYMBOL, Name, Series, Date,  MarketLot
    FROM @TypSecuritiesTable AS ts
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Securities AS s
        WHERE s.SYMBOL = ts.SYMBOL
    );

    SET @RowsAdded = @@ROWCOUNT;

    -- Total rows after operation
    SELECT @TotalRows = COUNT(*)
    FROM dbo.Securities;
END;


");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
