namespace UserAnalyticsService.Models
{
    public class Security :GenericDocument
    {
        public int? SecurityId { get; set; }
        public string? SecurityName { get; set; }
        public SecurityTypeEnum? SecurityType { get; set; }
    }

    public enum SecurityTypeEnum
    {
        Equity = 1,
        SME = 2,
        Index = 3
    }
}
