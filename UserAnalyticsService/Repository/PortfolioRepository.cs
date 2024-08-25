using MongoDB.Driver;
using UserAnalyticsService.DTOs;
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

        public async Task UpdatePresentPricesAsync(List<PriceData> priceDataList)
        {
            var portfolios = await Portfolios.Find(_ => true).ToListAsync();

            foreach (var portfolio in portfolios)
            {
                bool portfolioUpdated = false;

                foreach (var stock in portfolio.Stocks)
                {
                    // Find the latest price data for the stock based on the latest date
                    var latestPriceData = priceDataList
                        .Where(p => p.SecurityID == stock.SecurityId)
                        .OrderByDescending(p => p.Date)
                        .FirstOrDefault(); // Get the most recent price data for this security

                    if (latestPriceData != null)
                    {
                        stock.PresentPrice = latestPriceData.LTP; // Use LTP as the latest price

                    }
                }
                portfolioUpdated = true; // Mark portfolio as updated

                // Update portfolio in database if any stock was updated
                if (portfolioUpdated)
                {
                    portfolio.UpdateValues();
                    var filter = Builders<Portfolio>.Filter.Eq(p => p.Id, portfolio.Id);
                    await Portfolios.ReplaceOneAsync(filter, portfolio);
                }
            }
        }
    }
}
