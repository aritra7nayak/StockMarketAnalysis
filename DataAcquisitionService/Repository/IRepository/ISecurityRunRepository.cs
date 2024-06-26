using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ISecurityRunRepository : IGenericRepository<SecurityRun>
    {
        Task<SecurityRun> ProcessNSESecuritiesAsync(SecurityRun securityRun, DataTable securitiesTable);
        Task<SecurityRun> ProcessBSESecuritiesAsync(SecurityRun securityRun, DataTable securitiesTable);
    }
}
