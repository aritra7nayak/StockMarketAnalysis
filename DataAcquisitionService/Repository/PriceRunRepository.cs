using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class PriceRunRepository : GenericRepository<PriceRun>, IPriceRunRepository
    {

        public PriceRunRepository(AppDbContext context) : base(context)
        {
        }

        public Task<PriceRun> ProcessBSEPricesAsync(PriceRun priceRun, DataTable pricesTable)
        {
            throw new NotImplementedException();
        }

        public Task<PriceRun> ProcessNSEPricesAsync(PriceRun priceRun, DataTable pricesTable)
        {
            throw new NotImplementedException();
        }
    }
}
