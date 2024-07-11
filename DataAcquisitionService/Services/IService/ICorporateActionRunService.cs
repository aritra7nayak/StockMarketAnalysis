using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ICorporateActionRunService
    {
        Task<IEnumerable<CorporateActionRun>> GetAllCorporateActionRunsAsync();
        Task<IEnumerable<CorporateActionRun>> GetFilteredCorporateActionRunsAsync(CorporateActionRunFilterDto corporateActionRunFilterDto);
        Task<CorporateActionRun> GetCorporateActionRunByIdAsync(int id);
        Task AddCorporateActionRunAsync(CorporateActionRun corporateActionRun);
        Task UpdateCorporateActionRunAsync(CorporateActionRun corporateActionRun);
        Task DeleteCorporateActionRunAsync(int id);
    }
}
