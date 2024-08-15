using DataAcquisitionService.Dtos.SyncDto;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class SyncService : ISyncService
    {
        private readonly ISyncRepository _syncRepository;

        public SyncService(ISyncRepository syncRepository)
        {
            _syncRepository = syncRepository;
        }

        public SyncPriceResponseViewModel GetPriceData(DateTime date)
        {
            return _syncRepository.GetPriceData(date);
        }

        public SyncSecurityResponseViewModel GetSecurityData(DateTime date)
        {
            return _syncRepository.GetSecurityData(date);
        }
    }
}
