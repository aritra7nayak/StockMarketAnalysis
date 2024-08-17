using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ISecuritySyncService
    {
        Task<ResponseDto?> GetAllSecuritysAsync();
        Task<ResponseDto?> AddSecurityAsync(SecuritySyncProcessRuns securitySyncProcessRuns);
    }
}
