namespace StockAnalysis.Web.Models
{
    public class Portfolio : GenericDocument
    {
        public string? Owner { get; set; }

        public string? Name { get; set; }


        public List<Stock>? Stocks { get; set; } = new List<Stock>();

        public decimal? BuyValue { get; set; }
        public decimal? NowValue { get; set; }
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
