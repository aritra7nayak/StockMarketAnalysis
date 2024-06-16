using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ISecurityRunService
    {
        Task<IEnumerable<SecurityRun>> GetAllSecurityRunsAsync();
        Task<IEnumerable<SecurityRun>> GetFilteredSecurityRunsAsync(SecurityRunFilterDto securityRunFilterDto);
        Task<SecurityRun> GetSecurityRunByIdAsync(int id);
        Task AddSecurityRunAsync(SecurityRun securityRun);
        Task UpdateSecurityRunAsync(SecurityRun securityRun);
        Task DeleteSecurityRunAsync(int id);
    }
}
