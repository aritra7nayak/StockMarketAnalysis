using MongoDB.Bson;
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
        private readonly IMongoCollection<Security> _securitiesCollection;

        public PriceSyncRepository(DBContext dbContext)
        {
            _priceCollection = dbContext.Database.GetCollection<Price>("Prices");
            _securitiesCollection = dbContext.Database.GetCollection<Security>("Securities");
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

        public async Task<List<int?>> FindMatchingSecuritiesAsync(string securityNamePart)
        {

            var filter = Builders<Security>.Filter.Regex(s => s.SecurityName, new BsonRegularExpression(securityNamePart, "i"));
            var result = await _securitiesCollection.Find(filter).ToListAsync();

            return result.Select(s => s.SecurityId).ToList();
        }

        public async Task<List<SecurityAutoCompleteDto>> GetSecuritiesAutocompleteAsync(string securityNamePart)
        {
            // Step 1: Find matching securities
            var securityIds = await FindMatchingSecuritiesAsync(securityNamePart);

            if (securityIds.Count == 0)
            {
                return new List<SecurityAutoCompleteDto>();
            }

            var pipeline = new BsonDocument[]
            {
        // Match documents in the Prices collection for the found SecurityIDs
        new BsonDocument("$match", new BsonDocument("SecurityID", new BsonDocument("$in", new BsonArray(securityIds)))),
        
        // Sort documents by Date in descending order
        new BsonDocument("$sort", new BsonDocument("Date", -1)),

        // Group by SecurityID and Exchange, and get the latest document
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", new BsonDocument { { "SecurityID", "$SecurityID" }, { "Exchange", "$Exchange" } } },
            { "LatestStock", new BsonDocument("$first", "$$ROOT") }
        }),

        // Replace root with the latest document
        new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$LatestStock")),

        // Lookup the Securities collection to join on SecurityID
        new BsonDocument("$lookup", new BsonDocument
        {
            { "from", "Securities" },
            { "localField", "SecurityID" },
            { "foreignField", "SecurityId" },
            { "as", "SecuritiesInfo" }
        }),

        // Unwind the SecuritiesInfo array
        new BsonDocument("$unwind", "$SecuritiesInfo"),

        // Project the desired fields
        new BsonDocument("$project", new BsonDocument
        {
            { "SecurityID", 1 },
            { "Name", "$SecuritiesInfo.SecurityName" },
            { "Exchange", 1 },
            { "LatestPrice", "$LTP" },  // Assuming LTP is the latest price
            { "_id", 0 }  // Exclude _id from the output
        })
            };

            // Execute the pipeline
            var result = await _priceCollection.AggregateAsync<SecurityAutoCompleteDto>(pipeline);

            // Convert the result to a list
            return await result.ToListAsync();
        }



    }
}