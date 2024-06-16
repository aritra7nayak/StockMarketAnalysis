using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ISecurityRunService
    {
        Task<ResponseDto?> GetAllSecurityRunsAsync();
        Task<ResponseDto?> GetSecurityRunByIdAsync(int id);
        Task<ResponseDto?> AddSecurityRunAsync(SecurityRunDto securityRunDto);
        Task<ResponseDto?> UpdateSecurityRunAsync(SecurityRunDto securityRunDto);
        Task<ResponseDto?> DeleteSecurityAsync(int id);
    }
}
