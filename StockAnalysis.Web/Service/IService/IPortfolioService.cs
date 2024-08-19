using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IPortfolioService
    {
        Task<ResponseDto?> GetSecurityAutoComplete(string name);

    }
}
