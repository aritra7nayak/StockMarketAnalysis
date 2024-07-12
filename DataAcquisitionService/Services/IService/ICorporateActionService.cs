using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ICorporateActionService
    {
        Task<IEnumerable<CorporateAction>> GetAllCorporateActionsAsync();
        Task<CorporateAction> GetCorporateActionByIdAsync(int id);
        Task AddCorporateActionAsync(CorporateAction corporateAction);
        Task UpdateCorporateActionAsync(CorporateAction corporateAction);
        Task DeleteCorporateActionAsync(int id);
        Task<IEnumerable<CorporateAction>> GetFilteredCorporateActionAsync(string name);
    }
}
