using DataAcquisitionService.Data;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionTypeRunRepository : GenericRepository<CorporateActionTypeRunRepository>, ICorporateActionTypeRunRepository
    {
        public CorporateActionTypeRunRepository(AppDbContext context) : base(context)
        {
        }

    }
}