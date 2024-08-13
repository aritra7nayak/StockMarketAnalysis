using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
        {
        }
    }
}
