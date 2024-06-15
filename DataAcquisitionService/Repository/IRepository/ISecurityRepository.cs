using DataAcquisitionService.Models;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ISecurityRepository : IGenericRepository<Security>
    {
        Task<IEnumerable<Security>> GetFilteredSecurityAsync(string name, string symbol);

    }
}
