using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserAnalyticsService.Models
{
    public class Portfolio: GenericDocument
    {
        public string Owner { get; set; }

        public List<Stock>? Stocks { get; set; } = new List<Stock>();

        public decimal? BuyValue
        {
            get
            {
                decimal totalBuyValue = 0;
                foreach (var stock in Stocks)
                {
                    totalBuyValue += stock.BuyPrice * stock.Quantity;
                }
                return totalBuyValue;
            }
        }

        public decimal? NowValue
        {
            get
            {
                decimal totalNowValue = 0;
                foreach (var stock in Stocks)
                {
                    totalNowValue += stock.PresentPrice * stock.Quantity;
                }
                return totalNowValue;
            }
        }
    }

    public class Stock
    {
        public int SecurityId { get; set; }
        public int SecurityName { get; set; }
        public decimal BuyPrice { get; set; }
        public int Quantity { get; set; }
        public decimal PresentPrice { get; set; }
    }
}
