namespace UserAnalyticsService.Models
{
    public class Price :GenericDocument
    {

        public DateTime? Date { get; set; }
        public int? SecurityID { get; set; }
        public ExchangeEnum? Exchange { get; set; }

        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? LTP { get; set; }
        public decimal? PrevClose { get; set; }
    }
}
