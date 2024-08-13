using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;
using UserAnalyticsService.Repository;
using Microsoft.Extensions.Options;

namespace UserAnalyticsService.Data
{
    public class DBContext
    {
        private readonly IMongoDatabase _database;

        public DBContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        public IPortfolioRepository Portfolios => new PortfolioRepository(_database, "Portfolios");

    }
}
