using MongoDB.Driver;
using UserAnalyticsService.Data;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class SecuritySyncRepository : ISecuritySyncRepository
    {
        private readonly IMongoCollection<Security> _securityCollection;

        public SecuritySyncRepository(DBContext dbContext)
        {
            _securityCollection = dbContext.Database.GetCollection<Security>("Securities");
        }

        public async Task<SecuritySyncRun> StoreSecuritiesAsync(List<SecurityData> securities)
        {
            SecuritySyncRun securitySyncRun = new SecuritySyncRun();
            foreach (var securityData in securities)
            {
                var filter = Builders<Security>.Filter.Eq(s => s.SecurityId, securityData.SecurityId);

                // Check if the document exists
                var existingSecurity = await _securityCollection.Find(filter).FirstOrDefaultAsync();

                if (existingSecurity != null)
                {
                    // If it exists, update the fields
                    var update = Builders<Security>.Update
                                .Set(nameof(Security.SecurityName), securityData.SecurityName)
                                .Set(nameof(Security.SecurityType), securityData.SecurityType);

                    await _securityCollection.UpdateOneAsync(filter, update);
                    securitySyncRun.RowsUpdated += 1;
                }
                else
                {
                    // If it doesn't exist, insert a new document
                    var newSecurity = new Security
                    {
                        SecurityId = securityData.SecurityId,
                        SecurityName = securityData.SecurityName,
                        SecurityType = (SecurityTypeEnum?)securityData.SecurityType
                    };

                    await _securityCollection.InsertOneAsync(newSecurity);
                    securitySyncRun.RowsUpdated += 1;
                }
            }
            return securitySyncRun;
        }
    }

}
