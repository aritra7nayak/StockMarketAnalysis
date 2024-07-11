using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ICorporateActionTypeService
    {
        Task<IEnumerable<CorporateActionType>> GetAllCorporateActionTypesAsync();
        Task<CorporateActionType> GetCorporateActionTypeByIdAsync(int id);
        Task AddCorporateActionTypeAsync(CorporateActionType corporateActionType);
        Task UpdateCorporateActionTypeAsync(CorporateActionType corporateActionType);
        Task DeleteCorporateActionTypeAsync(int id);
        Task<IEnumerable<CorporateActionType>> GetFilteredCorporateActionTypeAsync(string name);
    }
}
