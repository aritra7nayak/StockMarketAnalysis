using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ICorporateActionTypeRunService
    {
        Task<IEnumerable<CorporateActionTypeRun>> GetAllCorporateActionTypeRunsAsync();
        Task<IEnumerable<CorporateActionTypeRun>> GetFilteredCorporateActionTypeRunsAsync(CorporateActionTypeRunFilterDto corporateActionTypeRunFilterDto);
        Task<CorporateActionTypeRun> GetCorporateActionTypeRunByIdAsync(int id);
        Task AddCorporateActionTypeRunAsync(CorporateActionTypeRun corporateActionTypeRun);
        Task UpdateCorporateActionTypeRunAsync(CorporateActionTypeRun corporateActionTypeRun);
        Task DeleteCorporateActionTypeRunAsync(int id);
    }
}
