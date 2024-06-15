using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace DataAcquisitionService.Repository
{
    public class SecurityRepository : GenericRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Security>> GetFilteredSecurityAsync(string name, string symbol)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(symbol))
            {
                query = query.Where(c => c.Symbol.Contains(symbol));
            }

            return await query.ToListAsync();
        }

    }
}
