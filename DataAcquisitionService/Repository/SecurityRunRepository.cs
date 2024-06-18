using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class SecurityRunRepository : GenericRepository<SecurityRun>, ISecurityRunRepository
    {
        private readonly AppDbContext _context;

        public SecurityRunRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SecurityRun> ProcessSecuritiesAsync(SecurityRun securityRun ,DataTable securitiesTable)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();
            await using (connection)
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
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
                    securityRun.RowsUpdated = (int)totalRowsParam.Value;
                    securityRun.RowsUpdated = (int)rowsUpdatedParam.Value;
                    securityRun.RowsDeleted = (int)rowsDeletedParam.Value;

                    return securityRun;
                }
            }
        }
    }
}
