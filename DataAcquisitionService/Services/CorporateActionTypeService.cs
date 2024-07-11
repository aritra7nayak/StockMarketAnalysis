using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class CorporateActionTypeService: ICorporateActionTypeService
    {
        private readonly IUnitofWork _unitOfWork;

        public CorporateActionTypeService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CorporateActionType>> GetAllCorporateActionTypesAsync()
        {
            return await _unitOfWork.corporateActionTypeRepository.GetAllAsync();
        }

        public async Task<CorporateActionType> GetCorporateActionTypeByIdAsync(int id)
        {
            return await _unitOfWork.corporateActionTypeRepository.GetByIdAsync(id);
        }

        public async Task AddCorporateActionTypeAsync(CorporateActionType corporateActionType)
        {
            try
            {

                await _unitOfWork.corporateActionTypeRepository.AddAsync(corporateActionType);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateCorporateActionTypeAsync(CorporateActionType corporateActionType)
        {
            try
            {
                await _unitOfWork.corporateActionTypeRepository.UpdateAsync(corporateActionType);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteCorporateActionTypeAsync(int id)
        {
            var corporateActionType = await _unitOfWork.corporateActionTypeRepository.GetByIdAsync(id);
            if (corporateActionType != null)
            {
                await _unitOfWork.corporateActionTypeRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CorporateActionType>> GetFilteredCorporateActionTypeAsync(string name)
        {
            //return await _unitOfWork.corporateActionTypeRepository.GetFilteredCorporateActionTypeAsync(name);
            throw new NotImplementedException();

        }
    }
}