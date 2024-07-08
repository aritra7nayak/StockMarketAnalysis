using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IPriceRunService
    {
        Task<ResponseDto?> GetAllPriceRunsAsync();
        Task<ResponseDto?> GetPriceRunByIdAsync(int id);
        Task<ResponseDto?> AddPriceRunAsync(PriceRunDto priceRunDto);
        Task<ResponseDto?> UpdatePriceRunAsync(PriceRunDto priceRunDto);
        Task<ResponseDto?> DeletePriceAsync(int id);
    }
}
