using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface IPriceService
    {
        Task<IEnumerable<Price>> GetAllPricesAsync();
        Task<Price> GetPriceByIdAsync(int id);
        Task AddPriceAsync(Price price);
        Task UpdatePriceAsync(Price price);
        Task DeletePriceAsync(int id);
        Task<IEnumerable<Price>> GetFilteredPriceAsync(string name);
    }
}
