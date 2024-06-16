namespace DataAcquisitionService.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISecurityRepository securityRepository { get; }
        ISecurityRunRepository securityRunRepository { get; }
        ICorporateAnnouncementRepository corporateAnnouncementRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
