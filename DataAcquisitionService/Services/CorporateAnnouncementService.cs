using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class CorporateAnnouncementService: ICorporateAnnouncementService
    {
        private readonly IUnitofWork _unitOfWork;

        public CorporateAnnouncementService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CorporateAnnouncement>> GetAllcorporateAnnouncementRepositoryAsync()
        {
            return await _unitOfWork.corporateAnnouncementRepository.GetAllAsync();
        }

        public async Task<CorporateAnnouncement> GetCorporateAnnouncementByIdAsync(int id)
        {
            return await _unitOfWork.corporateAnnouncementRepository.GetByIdAsync(id);
        }

        public async Task AddCorporateAnnouncementAsync(CorporateAnnouncement corporateAnnouncement)
        {
            await _unitOfWork.corporateAnnouncementRepository.AddAsync(corporateAnnouncement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCorporateAnnouncementAsync(CorporateAnnouncement corporateAnnouncement)
        {
            await _unitOfWork.corporateAnnouncementRepository.UpdateAsync(corporateAnnouncement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCorporateAnnouncementAsync(int id)
        {
            var corporateAnnouncement = await _unitOfWork.corporateAnnouncementRepository.GetByIdAsync(id);
            if (corporateAnnouncement != null)
            {
                await _unitOfWork.corporateAnnouncementRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CorporateAnnouncement>> GetAllCorporateAnnouncementsAsync()
        {
            return await _unitOfWork.corporateAnnouncementRepository.GetAllAsync();
        }
    }
}
