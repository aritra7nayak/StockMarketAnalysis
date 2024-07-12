using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class CorporateActionService:ICorporateActionService
    {
        private readonly IUnitofWork _unitOfWork;

        public CorporateActionService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CorporateAction>> GetAllCorporateActionsAsync()
        {
            return await _unitOfWork.corporateActionRepository.GetAllAsync();
        }

        public async Task<CorporateAction> GetCorporateActionByIdAsync(int id)
        {
            return await _unitOfWork.corporateActionRepository.GetByIdAsync(id);
        }

        public async Task AddCorporateActionAsync(CorporateAction corporateAction)
        {
            try
            {

                await _unitOfWork.corporateActionRepository.AddAsync(corporateAction);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateCorporateActionAsync(CorporateAction corporateAction)
        {
            try
            {
                await _unitOfWork.corporateActionRepository.UpdateAsync(corporateAction);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteCorporateActionAsync(int id)
        {
            var corporateAction = await _unitOfWork.corporateActionRepository.GetByIdAsync(id);
            if (corporateAction != null)
            {
                await _unitOfWork.corporateActionRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CorporateAction>> GetFilteredCorporateActionAsync(string name)
        {
            //return await _unitOfWork.corporateActionRepository.GetFilteredCorporateActionAsync(name);
            throw new NotImplementedException();

        }
    }
}