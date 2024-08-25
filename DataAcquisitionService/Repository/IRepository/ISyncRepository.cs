using DataAcquisitionService.Dtos.SyncDto;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface ISyncRepository
    {
        SyncSecurityResponseViewModel GetSecurityData(DateTime date);

        SyncPriceResponseViewModel GetPriceData(DateTime date);


    }
}
