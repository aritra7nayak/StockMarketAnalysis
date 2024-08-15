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

        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        }

        public IMongoDatabase Database => _database;

    }
}
