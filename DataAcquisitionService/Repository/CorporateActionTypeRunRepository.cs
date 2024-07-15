using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionTypeRunRepository : GenericRepository<CorporateActionTypeRun>, ICorporateActionTypeRunRepository
    {
        public CorporateActionTypeRunRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CorporateActionTypeRun> ProcessNSECorporateActionTypesAsync(CorporateActionTypeRun corporateActionTypeRun, DataTable corporateActionTypesTable)
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
                    command.CommandText = "dbo.spNSECorporateActionTypeImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypNSECorporateActionsTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeNSECorporateActions",
                        Value = corporateActionTypesTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = corporateActionTypeRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = corporateActionTypeRun.Id });

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
                    corporateActionTypeRun.RowsAdded = (int)rowsAddedParam.Value;
                    corporateActionTypeRun.RowsTotal = (int)totalRowsParam.Value;
                    corporateActionTypeRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    corporateActionTypeRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return corporateActionTypeRun;
        }
    }
}