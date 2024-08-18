namespace StockAnalysis.Web.Models
{
    public class PriceSyncProcessRuns : GenericDocument
    {
        public bool IsSuccess { get; set; }
        public DateTime ProcessUpdateTillDate { get; set; }
        public DateTime UpdateTillDate { get; set; }

        public int? RowsTotal { get; set; }
        public int? RowsAdded { get; set; }
        public int? RowsUpdated { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
