using DataAcquisitionService.Data;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAcquisitionService.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            securityRepository = new SecurityRepository(appDbContext);
            securityRunRepository = new SecurityRunRepository(appDbContext);
            corporateAnnouncementRepository = new CorporateAnnouncementRepository(appDbContext);

        }

        public ISecurityRepository securityRepository { get; private set; }
        public ISecurityRunRepository securityRunRepository { get; private set; }
        public ICorporateAnnouncementRepository corporateAnnouncementRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
