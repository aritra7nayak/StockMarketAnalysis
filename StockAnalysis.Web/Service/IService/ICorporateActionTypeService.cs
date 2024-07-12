using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ICorporateActionTypeService
    {
        Task<ResponseDto?> GetAllCorporateActionTypesAsync();
        Task<ResponseDto?> GetCorporateActionTypeByIdAsync(int id);
        Task<ResponseDto?> AddCorporateActionTypeAsync(CorporateActionType corporateActionType);
        Task<ResponseDto?> UpdateCorporateActionTypeAsync(CorporateActionType corporateActionType);
        Task<ResponseDto?> DeleteCorporateActionTypeAsync(int id);
        Task<ResponseDto?> GetCorporateActionTypesbyNameorSymbolAsync(string name);
    }
}
