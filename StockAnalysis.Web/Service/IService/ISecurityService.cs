using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ISecurityService
    {
        Task<ResponseDto?> GetAllSecuritysAsync();
        Task<ResponseDto?> GetSecurityByIdAsync(int id);
        Task<ResponseDto?> AddSecurityAsync(Security security);
        Task<ResponseDto?> UpdateSecurityAsync(Security security);
        Task<ResponseDto?> DeleteSecurityAsync(int id);
        Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name);
    }
}
