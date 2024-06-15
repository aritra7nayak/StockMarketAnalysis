using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.IService
{
    public interface ICorporateAnnouncementService
    {
        Task<IEnumerable<CorporateAnnouncement>> GetAllCorporateAnnouncementsAsync();
        Task<CorporateAnnouncement> GetCorporateAnnouncementByIdAsync(int id);
        Task AddCorporateAnnouncementAsync(CorporateAnnouncement corporateAnnouncement);
        Task UpdateCorporateAnnouncementAsync(CorporateAnnouncement corporateAnnouncement);
        Task DeleteCorporateAnnouncementAsync(int id);
    }
}
