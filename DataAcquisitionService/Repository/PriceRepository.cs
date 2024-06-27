using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext context) : base(context)
        {
        }

    }
}
