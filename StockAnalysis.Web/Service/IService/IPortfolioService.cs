using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IPortfolioService
    {
        Task<ResponseDto?> GetSecurityAutoComplete(string name);
        Task<ResponseDto?> GetUserPortfoliosAsync();
        Task<ResponseDto?> GetPortfolioByIdAsync(Guid id);
        Task<ResponseDto?> AddPortfolioAsync(Portfolio portfolio);
        Task<ResponseDto?> UpdatePortfolioAsync(Portfolio portfolio);
        Task<ResponseDto?> DeletePortfolioAsync(int id);

    }
}
