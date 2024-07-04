namespace StockAnalysis.Web.Models
{
    public class Price
    {
        public int? ID { get; set; }

        #region Candidate Key
        public DateTime? Date { get; set; }
        public int? SecurityID { get; set; }
        public ExchangeEnum? Exchange { get; set; }
        #endregion

        public int? PriceRunID { get; set; }


        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? LTP { get; set; }
        public decimal? TradedVolume { get; set; }
    }
}
