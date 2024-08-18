using MongoDB.Driver;
using UserAnalyticsService.Data;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class PriceSyncRepository : IPriceSyncRepository
    {
        private readonly IMongoCollection<Price> _securityCollection;

        public PriceSyncRepository(DBContext dbContext)
        {
            _securityCollection = dbContext.Database.GetCollection<Price>("Prices");
        }
        public Task StorePricesAsync(List<PriceData> securities)
        {
            throw new NotImplementedException();
        }
    }
}
