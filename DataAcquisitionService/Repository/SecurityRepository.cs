using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class SecurityRepository : GenericRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
