using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionRunRepository : GenericRepository<CorporateActionRun>, ICorporateActionRunRepository
    {
        public CorporateActionRunRepository(AppDbContext context) : base(context)
        {
        }

    }
}