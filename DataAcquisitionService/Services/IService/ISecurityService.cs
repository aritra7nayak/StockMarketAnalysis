using DataAcquisitionService.Models;
using Stripe;

namespace DataAcquisitionService.Services.IService
{
    public interface ISecurityService
    {
        Task<IEnumerable<Security>> GetAllSecuritysAsync();
        Task<Security> GetSecurityByIdAsync(int id);
        Task AddSecurityAsync(Security security);
        Task UpdateSecurityAsync(Security security);
        Task DeleteSecurityAsync(int id);
        Task<IEnumerable<Security>> GetFilteredSecurityAsync(string name, string symbol);
    }
}
