using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace UserAnalyticsService
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        private static bool _isMongoDbConfigured = false;

        public static void ConfigureMongoDb()
        {
            if (!_isMongoDbConfigured)
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                _isMongoDbConfigured = true;
            }
        }
    }
}
