using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ISecurityRunRepository : IGenericRepository<SecurityRun>
    {
        Task<SecurityRun> ProcessSecuritiesAsync(SecurityRun securityRun, DataTable securitiesTable);
    }
}
