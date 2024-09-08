using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class BankDepositRepository: GenericRepository<BankDeposit>, IBankDepositRepository
    {
        private readonly IMongoDatabase _database;

        public BankDepositRepository(IMongoDatabase database)
            : base(database, "BankDeposits") // "BankDeposits" is the collection name in MongoDB
        {
            _database = database;
        }

        public IMongoCollection<BankDeposit> BankDeposits => _collection;
    }
}
