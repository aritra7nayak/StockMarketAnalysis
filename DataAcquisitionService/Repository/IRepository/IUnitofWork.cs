namespace DataAcquisitionService.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISecurityRepository securityRepository { get; }
        ICorporateAnnouncementRepository corporateAnnouncementRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
