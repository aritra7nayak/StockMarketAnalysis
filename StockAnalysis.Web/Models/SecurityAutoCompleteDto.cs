namespace StockAnalysis.Web.Models
{
    public class SecurityAutoCompleteDto
    {
        public int? SecurityID { get; set; }
        public string Name { get; set; }
        public int? Exchange { get; set; }

        public decimal? LatestPrice { get; set; }
    }
}
