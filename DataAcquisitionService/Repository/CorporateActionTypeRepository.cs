using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionTypeRepository : GenericRepository<CorporateActionType>, ICorporateActionTypeRepository
    {
        public CorporateActionTypeRepository(AppDbContext context) : base(context)
        {
        }

    }
}