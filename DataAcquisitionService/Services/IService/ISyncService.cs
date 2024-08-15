using DataAcquisitionService.Dtos.SyncDto;

namespace DataAcquisitionService.Services.IService
{
    public interface ISyncService
    {
        SyncSecurityResponseViewModel GetSecurityData(DateTime date);

        SyncPriceResponseViewModel GetPriceData(DateTime date);
    }
}
