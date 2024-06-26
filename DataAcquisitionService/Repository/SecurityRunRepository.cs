using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DataAcquisitionService.Models;
using DataAcquisitionService.Data;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAcquisitionService.Repository
{
    public class SecurityRunRepository : GenericRepository<SecurityRun>, ISecurityRunRepository
    {

        public SecurityRunRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SecurityRun> ProcessBSESecuritiesAsync(SecurityRun securityRun, DataTable securitiesTable)

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
                    command.CommandText = "dbo.spBSESecurityImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypBSESecuritiesTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeBSESecurities",
                        Value = securitiesTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = securityRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = securityRun.Id });

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
                    securityRun.RowsAdded = (int)rowsAddedParam.Value;
                    securityRun.RowsTotal = (int)totalRowsParam.Value;
                    securityRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    securityRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return securityRun;
        }

        public async Task<SecurityRun> ProcessNSESecuritiesAsync(SecurityRun securityRun, DataTable securitiesTable)
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
                    command.CommandText = "dbo.spSecurityImporter";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add table type parameter
                    var tableParam = new SqlParameter("@TypSecuritiesTable", SqlDbType.Structured)
                    {
                        TypeName = "dbo.TypeSecurities",
                        Value = securitiesTable
                    };
                    command.Parameters.Add(tableParam);

                    // Add other parameters
                    command.Parameters.Add(new SqlParameter("@RunDate", SqlDbType.Date) { Value = securityRun.Date });
                    command.Parameters.Add(new SqlParameter("@RunId", SqlDbType.Int) { Value = securityRun.Id });

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
                    securityRun.RowsAdded = (int)rowsAddedParam.Value;
                    securityRun.RowsTotal = (int)totalRowsParam.Value;
                    securityRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    securityRun.RowsDeleted = (int)rowsDeletedParam.Value;
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return securityRun;
        }
    }
}
