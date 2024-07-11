using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionRunRepository : GenericRepository<CorporateActionRun>, ICorporateActionRunRepository
    {
        public CorporateActionRunRepository(AppDbContext context) : base(context)
        {
        }

        public Task<CorporateActionRun> ProcessNSECorporateActionsAsync(CorporateActionRun corporateActionRun, DataTable corporateActionsTable)
        {
            throw new NotImplementedException();
        }
    }
}