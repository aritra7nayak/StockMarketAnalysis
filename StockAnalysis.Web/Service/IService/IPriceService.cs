using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IPriceService
    {
        Task<ResponseDto?> GetAllPricesAsync();
        Task<ResponseDto?> GetPriceByIdAsync(int id);
        Task<ResponseDto?> AddPriceAsync(Price price);
        Task<ResponseDto?> UpdatePriceAsync(Price price);
        Task<ResponseDto?> DeletePriceAsync(int id);
        Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name);
    }
}
