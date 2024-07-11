using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using System.Data;

namespace DataAcquisitionService.Repository
{
    public class CorporateActionTypeRunRepository : GenericRepository<CorporateActionTypeRun>, ICorporateActionTypeRunRepository
    {
        public CorporateActionTypeRunRepository(AppDbContext context) : base(context)
        {
        }

        public Task<CorporateActionTypeRun> ProcessNSECorporateActionTypesAsync(CorporateActionTypeRun corporateActionTypeRun, DataTable corporateActionTypesTable)
        {
            throw new NotImplementedException();
        }
    }
}