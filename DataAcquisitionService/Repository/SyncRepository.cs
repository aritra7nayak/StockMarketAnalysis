using DataAcquisitionService.Data;
using DataAcquisitionService.Dtos.SyncDto;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class SyncRepository : ISyncRepository
    {
        private readonly AppDbContext _context;

        public SyncRepository(AppDbContext context) 
        {
            _context = context;        
        }
        public SyncPriceResponseViewModel GetPriceData(DateTime date)
        {
            var priceData = _context.Prices.Where(w => w.ModifiedOn > date)
                               .Select(w => new PriceData
                               {
                                   Date = w.Date,
                                   SecurityID = w.SecurityID,
                                   Exchange = (int?)w.Exchange,
                                   Open = w.Open,
                                   High = w.High,
                                   Low = w.Low,
                                   Close = w.Close,
                                   LTP = w.LTP,
                                   PrevClose = w.PrevClose,
                                   UpdatedOn = w.ModifiedOn
                               }).ToList();
            SyncPriceResponseViewModel result = new SyncPriceResponseViewModel();
            result.Data = priceData;
            result.LastUpdatedDate = (DateTime)priceData.Max(d => d.UpdatedOn);

            return result;
        }

        public SyncSecurityResponseViewModel GetSecurityData(DateTime date)
        {
            var securityData = _context.Securities.Where(w => w.ModifiedOn > date)
                               .Select(w => new SecurityData
                               {
                                   SecurityId = w.ID,
                                   SecurityName = w.Name,
                                   SecurityType = (int?)w.SecurityType,
                                   UpdatedOn = w.ModifiedOn
                               }).ToList();
            SyncSecurityResponseViewModel result = new SyncSecurityResponseViewModel();
            result.Data = securityData;
            result.LastUpdatedDate = (DateTime)securityData.Max(d => d.UpdatedOn);

            return result;
        }
    }
}
