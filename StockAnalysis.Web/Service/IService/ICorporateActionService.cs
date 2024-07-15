using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ICorporateActionService
    {
        Task<ResponseDto?> GetAllCorporateActionsAsync();
        Task<ResponseDto?> GetCorporateActionByIdAsync(int id);
        Task<ResponseDto?> AddCorporateActionAsync(CorporateAction corporateAction);
        Task<ResponseDto?> UpdateCorporateActionAsync(CorporateAction corporateAction);
        Task<ResponseDto?> DeleteCorporateActionAsync(int id);
        Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name);
    }
}
