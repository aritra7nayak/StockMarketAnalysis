using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface ICorporateActionRunService
    {
        Task<ResponseDto?> GetAllCorporateActionRunsAsync();
        Task<ResponseDto?> GetCorporateActionRunByIdAsync(int id);
        Task<ResponseDto?> AddCorporateActionRunAsync(CorporateActionRunDto corporateActionRunDto);
        Task<ResponseDto?> UpdateCorporateActionRunAsync(CorporateActionRunDto corporateActionRunDto);
        Task<ResponseDto?> DeleteCorporateActionAsync(int id);
    }
}
