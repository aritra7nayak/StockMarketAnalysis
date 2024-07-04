using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class PriceRunRepository : GenericRepository<PriceRun>, IPriceRunRepository
    {

        public PriceRunRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PriceRun> ProcessBSEPricesAsync(PriceRun priceRun, DataTable pricesTable)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get the connection from the context and open it if not already open
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using (var command = connection.CreateCommand())
                {
                    command.Transaction = (SqlTransaction)transaction.GetDbTransaction();
                    command.CommandText = "dbo.spBSEPriceImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypBSEPricesTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeBSEPrices",
                        Value = pricesTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = priceRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = priceRun.Id });

                    // Add output parameters
                    var rowsAddedParam = new SqlParameter("@RowsAdded", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsAddedParam);

                    var totalRowsParam = new SqlParameter("@TotalRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(totalRowsParam);

                    var rowsUpdatedParam = new SqlParameter("@RowsUpdated", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsUpdatedParam);

                    var rowsDeletedParam = new SqlParameter("@RowsDeleted", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsDeletedParam);

                    // Execute the command
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output parameter values
                    priceRun.RowsAdded = (int)rowsAddedParam.Value;
                    priceRun.RowsTotal = (int)totalRowsParam.Value;
                    priceRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    priceRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return priceRun;
        }

        public async Task<PriceRun> ProcessNSEPricesAsync(PriceRun priceRun, DataTable pricesTable)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get the connection from the context and open it if not already open
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using (var command = connection.CreateCommand())
                {
                    command.Transaction = (SqlTransaction)transaction.GetDbTransaction();
                    command.CommandText = "dbo.spNSEPriceImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypNSEPricesTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeNSEPrices",
                        Value = pricesTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = priceRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = priceRun.Id });

                    // Add output parameters
                    var rowsAddedParam = new SqlParameter("@RowsAdded", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsAddedParam);

                    var totalRowsParam = new SqlParameter("@TotalRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(totalRowsParam);

                    var rowsUpdatedParam = new SqlParameter("@RowsUpdated", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsUpdatedParam);

                    var rowsDeletedParam = new SqlParameter("@RowsDeleted", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(rowsDeletedParam);

                    // Execute the command
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output parameter values
                    priceRun.RowsAdded = (int)rowsAddedParam.Value;
                    priceRun.RowsTotal = (int)totalRowsParam.Value;
                    priceRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    priceRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return priceRun;
        }
    }
}
