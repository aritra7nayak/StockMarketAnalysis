namespace DataAcquisitionService.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISecurityRepository securityRepository { get; }
        ISecurityRunRepository securityRunRepository { get; }

        IPriceRepository priceRepository { get; }
        IPriceRunRepository priceRunRepository { get; }

        ICorporateActionTypeRepository corporateActionTypeRepository { get; }
        ICorporateActionTypeRunRepository corporateActionTypeRunRepository { get; }

        ICorporateActionRepository corporateActionRepository { get; }
        ICorporateActionRunRepository corporateActionRunRepository { get; }


        ICorporateAnnouncementRepository corporateAnnouncementRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
