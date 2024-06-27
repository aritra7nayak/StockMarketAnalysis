using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface IPriceRunService
    {
        Task<IEnumerable<PriceRun>> GetAllPriceRunsAsync();
        Task<IEnumerable<PriceRun>> GetFilteredPriceRunsAsync(PriceRunFilterDto priceRunFilterDto);
        Task<PriceRun> GetPriceRunByIdAsync(int id);
        Task AddPriceRunAsync(PriceRun priceRun);
        Task UpdatePriceRunAsync(PriceRun priceRun);
        Task DeletePriceRunAsync(int id);
    }
}
