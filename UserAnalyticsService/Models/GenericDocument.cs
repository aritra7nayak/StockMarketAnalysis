using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserAnalyticsService.Models
{
    public class GenericDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
