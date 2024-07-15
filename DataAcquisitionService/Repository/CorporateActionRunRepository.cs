using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionRunRepository : GenericRepository<CorporateActionRun>, ICorporateActionRunRepository
    {
        public CorporateActionRunRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CorporateActionRun> ProcessNSECorporateActionsAsync(CorporateActionRun corporateActionRun, DataTable corporateActionsTable)
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
                    command.CommandText = "dbo.spNSECorporateActionImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypNSECorporateActionsTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeNSECorporateActions",
                        Value = corporateActionsTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = corporateActionRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = corporateActionRun.Id });

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
                    corporateActionRun.RowsAdded = (int)rowsAddedParam.Value;
                    corporateActionRun.RowsTotal = (int)totalRowsParam.Value;
                    corporateActionRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    corporateActionRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return corporateActionRun;
        }
    }
}