using MongoDB.Driver;
using UserAnalyticsService.Data;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class PriceSyncRepository : IPriceSyncRepository
    {
        private readonly IMongoCollection<Price> _priceCollection;

        public PriceSyncRepository(DBContext dbContext)
        {
            _priceCollection = dbContext.Database.GetCollection<Price>("Prices");
        }
        public async Task<PriceSyncRun> StorePricesAsync(List<PriceData> prices)
        {
            PriceSyncRun priceSyncRun = new PriceSyncRun();
            priceSyncRun.RowsTotal = prices.Count;
            priceSyncRun.RowsUpdated = 0;
            priceSyncRun.RowsAdded = 0;

            foreach (var priceData in prices)
            {
                var filter = Builders<Price>.Filter.And(
                                                            Builders<Price>.Filter.Eq(s => s.SecurityID, priceData.SecurityID),
                                                            Builders<Price>.Filter.Eq(s => s.Date, priceData.Date),
                                                            Builders<Price>.Filter.Eq(s => (int?)s.Exchange, priceData.Exchange)

                                                        );
                // Check if the document exists
                var existingPrice = await _priceCollection.Find(filter).FirstOrDefaultAsync();

                if (existingPrice != null)
                {
                    // If it exists, update the fields
                    var update = Builders<Price>.Update
                                .Set(nameof(Price.Open), priceData.Open)
                                .Set(nameof(Price.High), priceData.High)
                                .Set(nameof(Price.Low), priceData.Low)
                                .Set(nameof(Price.Close), priceData.Close)
                                .Set(nameof(Price.LTP), priceData.LTP)
                                .Set(nameof(Price.PrevClose), priceData.PrevClose)
                                ;

                    await _priceCollection.UpdateOneAsync(filter, update);
                    priceSyncRun.RowsUpdated += 1;
                }
                else
                {
                    // If it doesn't exist, insert a new document
                    var newPrice = new Price
                    {
                        SecurityID = priceData.SecurityID,
                        Date = priceData.Date,
                        Exchange = (ExchangeEnum?)priceData.Exchange,
                        Open = priceData.Open,
                        High = priceData.High,
                        Low = priceData.Low,
                        Close = priceData.Close,
                        LTP = priceData.LTP,
                        PrevClose = priceData.PrevClose,
                    };

                    await _priceCollection.InsertOneAsync(newPrice);
                    priceSyncRun.RowsAdded += 1;
                }
            }
            return priceSyncRun;
        }
    }
}
