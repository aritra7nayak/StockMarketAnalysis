using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class SecurityRunService: ISecurityRunService
    {
        private readonly IUnitofWork _unitOfWork;

        public SecurityRunService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSecurityRunAsync(SecurityRun securityRun)
        {
            try
            {

                await _unitOfWork.securityRunRepository.AddAsync(securityRun);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }



        }
    

        public async Task DeleteSecurityRunAsync(int id)
        {
            await _unitOfWork.securityRunRepository.DeleteForParent<Security>(id);
        }

        public async Task<IEnumerable<SecurityRun>> GetAllSecurityRunsAsync()
        {
            return await _unitOfWork.securityRunRepository.GetAllAsync();
        }

        public async Task<IEnumerable<SecurityRun>> GetFilteredSecurityRunsAsync(SecurityRunFilterDto securityRunFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<SecurityRun> GetSecurityRunByIdAsync(int id)
        {
            return await _unitOfWork.securityRunRepository.GetByIdAsync(id);
        }

        public Task UpdateSecurityRunAsync(SecurityRun securityRun)
        {
            throw new NotImplementedException();
        }
    }
}
