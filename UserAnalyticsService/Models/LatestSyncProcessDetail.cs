using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserAnalyticsService.Models
{
    public class LatestSyncProcessDetail
    {
        [BsonId] // Marks this as the MongoDB ObjectId
        [BsonRepresentation(BsonType.ObjectId)] // Ensures the field is stored as ObjectId in MongoDB
        public string _id { get; set; }
        public SyncProcessTypeEnum SyncProcessType { get; set; }
        public DateTime ProcessUpdateTillDate { get; set; }
    }

    public enum SyncProcessTypeEnum
    {
        Security = 1,
        Price = 2
    }
}
