using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class SecurityRunRepository : GenericRepository<SecurityRun>, ISecurityRunRepository
    {
        public SecurityRunRepository(AppDbContext context) : base(context)
        {
        }
    }
}
