using DataAcquisitionService.Data;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionTypeRepository : GenericRepository<CorporateActionTypeRepository>, ICorporateActionTypeRepository
    {
        public CorporateActionTypeRepository(AppDbContext context) : base(context)
        {
        }

    }
}