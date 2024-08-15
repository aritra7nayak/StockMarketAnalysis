using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        private readonly IMongoDatabase _database;

        public PortfolioRepository(IMongoDatabase database)
            : base(database, "Portfolios") // "Portfolios" is the collection name in MongoDB
        {
            _database = database;
        }

        public IMongoCollection<Portfolio> Portfolios => _collection;
    }
}
