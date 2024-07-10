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

            priceRepository = new PriceRepository(appDbContext);
            priceRunRepository = new PriceRunRepository(appDbContext);

            corporateActionTypeRepository = new CorporateActionTypeRepository(appDbContext);
            corporateActionTypeRunRepository = new CorporateActionTypeRunRepository(appDbContext);
            corporateActionRepository = new CorporateActionRepository(appDbContext);
            corporateActionRunRepository = new CorporateActionRunRepository(appDbContext);
            corporateAnnouncementRepository = new CorporateAnnouncementRepository(appDbContext);

        }

        public ISecurityRepository securityRepository { get; private set; }
        public ISecurityRunRepository securityRunRepository { get; private set; }
        public IPriceRepository priceRepository { get; private set; }
        public IPriceRunRepository priceRunRepository { get; private set; }

        public ICorporateActionTypeRepository corporateActionTypeRepository { get; private set; }
        public ICorporateActionTypeRunRepository corporateActionTypeRunRepository { get; private set; }
        public ICorporateActionRepository corporateActionRepository { get; private set; }
        public ICorporateActionRunRepository corporateActionRunRepository { get; private set; }
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
