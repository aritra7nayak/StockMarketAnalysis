namespace DataAcquisitionService.Dtos.SyncDto
{
    public class SyncSecurityResponseViewModel
    {
        public bool Success { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Message { get; set; }
        public List<SecurityData> Data { get; set; }
    }

    public class SecurityData
    {
        public int? SecurityId { get; set; }
        public string? SecurityName { get; set; }
        public int? SecurityType { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class SyncPriceResponseViewModel
    {
        public bool Success { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public string Message { get; set; }
        public List<PriceData> Data { get; set; }
    }

    public class PriceData
    {
        public DateTime? Date { get; set; }
        public int? SecurityID { get; set; }
        public int? Exchange { get; set; }

        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? LTP { get; set; }
        public decimal? PrevClose { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
