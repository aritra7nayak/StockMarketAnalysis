using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ICorporateActionTypeRunService
    {
        Task<ResponseDto?> GetAllCorporateActionTypeRunsAsync();
        Task<ResponseDto?> GetCorporateActionTypeRunByIdAsync(int id);
        Task<ResponseDto?> AddCorporateActionTypeRunAsync(CorporateActionTypeRunDto corporateActionTypeRunDto);
        Task<ResponseDto?> UpdateCorporateActionTypeRunAsync(CorporateActionTypeRunDto corporateActionTypeRunDto);
        Task<ResponseDto?> DeleteCorporateActionTypeAsync(int id);
    }
}
