using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;
using Stripe;

namespace DataAcquisitionService.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitofWork _unitOfWork;

        public SecurityService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Security>> GetAllsecurityRepositoryAsync()
        {
            return await _unitOfWork.securityRepository.GetAllAsync();
        }

        public async Task<Security> GetSecurityByIdAsync(int id)
        {
            return await _unitOfWork.securityRepository.GetByIdAsync(id);
        }

        public async Task AddSecurityAsync(Security security)
        {
            try
            {

                await _unitOfWork.securityRepository.AddAsync(security);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateSecurityAsync(Security security)
        {
            try { 
            await _unitOfWork.securityRepository.UpdateAsync(security);
            await _unitOfWork.SaveChangesAsync();
        }
            catch (Exception ex)
            {

            }
}

        public async Task DeleteSecurityAsync(int id)
        {
            var security = await _unitOfWork.securityRepository.GetByIdAsync(id);
            if (security != null)
            {
                await _unitOfWork.securityRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Security>> GetFilteredSecurityAsync(string name)
        {
            return await _unitOfWork.securityRepository.GetFilteredSecurityAsync(name);
        }

        public async Task<IEnumerable<Security>> GetAllSecuritysAsync()
        {
            return await _unitOfWork.securityRepository.GetAllAsync();
        }
    }
}