namespace DataAcquisitionService.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISecurityRepository securityRepository { get; }
        ISecurityRunRepository securityRunRepository { get; }

        IPriceRepository priceRepository { get; }
        IPriceRunRepository priceRunRepository { get; }
        ICorporateAnnouncementRepository corporateAnnouncementRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
