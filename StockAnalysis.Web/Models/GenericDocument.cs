﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockAnalysis.Web.Models
{
    public class GenericDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
