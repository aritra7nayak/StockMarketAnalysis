using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionRepository : GenericRepository<CorporateAction>, ICorporateActionRepository
    {
        public CorporateActionRepository(AppDbContext context) : base(context)
        {
        }

    }
}