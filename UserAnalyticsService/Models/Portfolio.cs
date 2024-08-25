using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserAnalyticsService.Models
{
    public class Portfolio : GenericDocument
    {
        public string? Owner { get; set; }
        public string? Name { get; set; }

        public List<Stock>? Stocks { get; set; } = new List<Stock>();

        // Store BuyValue and NowValue directly in the database
        public decimal? BuyValue { get; set; }
        public decimal? NowValue { get; set; }

        // Method to update the BuyValue and NowValue fields
        public void UpdateValues()
        {
            BuyValue = CalculateBuyValue();
            NowValue = CalculateNowValue();
        }

        private decimal? CalculateBuyValue()
        {
            decimal? totalBuyValue = 0;
            foreach (var stock in Stocks)
            {
                totalBuyValue += stock.BuyPrice * stock.Quantity;
            }
            return totalBuyValue;
        }

        private decimal? CalculateNowValue()
        {
            decimal? totalNowValue = 0;
            foreach (var stock in Stocks)
            {
                totalNowValue += stock.PresentPrice * stock.Quantity;
            }
            return totalNowValue;
        }
    }

    public class Stock
    {
        public int? SecurityId { get; set; }
        public string? SecurityName { get; set; }
        public decimal? BuyPrice { get; set; }
        public int? Quantity { get; set; }
        public decimal? PresentPrice { get; set; }
    }
}
