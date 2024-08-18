namespace UserAnalyticsService.Models
{
    public class LatestSyncProcessDetail
    {
        public SyncProcessTypeEnum SyncProcessType { get; set; }
        public DateTime ProcessUpdateTillDate { get; set; }
    }

    public enum SyncProcessTypeEnum
    {
        Security = 1,
        Price = 2
    }
}
