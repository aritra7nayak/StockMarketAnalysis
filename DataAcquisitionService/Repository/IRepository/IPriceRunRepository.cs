using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface IPriceRunRepository : IGenericRepository<PriceRun>
    {
        Task<PriceRun> ProcessNSEPricesAsync(PriceRun priceRun, DataTable pricesTable);
        Task<PriceRun> ProcessBSEPricesAsync(PriceRun priceRun, DataTable pricesTable);
    }
}
