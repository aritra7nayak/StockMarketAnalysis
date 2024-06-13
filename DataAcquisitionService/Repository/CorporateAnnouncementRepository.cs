using DataAcquisitionService.Data;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;

namespace DataAcquisitionService.Repository
{
    public class CorporateAnnouncementRepository : GenericRepository<CorporateAnnouncement>, ICorporateAnnouncementRepository
    {
        public CorporateAnnouncementRepository(AppDbContext context) : base(context)
        {
        }
    }
}
