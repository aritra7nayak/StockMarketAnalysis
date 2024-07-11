using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ICorporateActionTypeRunRepository : IGenericRepository<CorporateActionTypeRun>
    {
        Task<CorporateActionTypeRun> ProcessNSECorporateActionTypesAsync(CorporateActionTypeRun corporateActionTypeRun, DataTable corporateActionTypesTable);
    }
}