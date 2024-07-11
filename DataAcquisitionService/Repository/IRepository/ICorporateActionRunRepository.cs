using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ICorporateActionRunRepository : IGenericRepository<CorporateActionRun>
    {
        Task<CorporateActionRun> ProcessNSECorporateActionsAsync(CorporateActionRun corporateActionRun, DataTable corporateActionsTable);
    }
}
