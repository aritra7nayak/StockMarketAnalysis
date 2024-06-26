using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.Importer;
using DataAcquisitionService.Services.IService;
using System.Data;

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
            try
            {
                switch (securityRun.SourceType)
                {
                    case SourceTypeEnum.NSE:
                        NSESecurityImporter securityImporter = new NSESecurityImporter(securityRun);
                        securityImporter.InitiateProcess();
                        DataTable dataTable = securityImporter.GetDataTable();
                        securityRun = await _unitOfWork.securityRunRepository.ProcessNSESecuritiesAsync(securityRun, dataTable);
                        await _unitOfWork.securityRunRepository.UpdateAsync(securityRun);
                        await _unitOfWork.SaveChangesAsync();
                        break;
                    case SourceTypeEnum.BSE:
                        BSESecurityImporter bseSecurityImporter = new BSESecurityImporter(securityRun);
                        bseSecurityImporter.InitiateProcess();
                        DataTable bsedataTable = bseSecurityImporter.GetDataTable();
                        securityRun = await _unitOfWork.securityRunRepository.ProcessBSESecuritiesAsync(securityRun, bsedataTable);
                        await _unitOfWork.securityRunRepository.UpdateAsync(securityRun);
                        await _unitOfWork.SaveChangesAsync();
                        break;

                }
                

            }
            catch (Exception ex)
            {

            }

        }
    

        public async Task DeleteSecurityRunAsync(int id)
        {
            await _unitOfWork.securityRunRepository.DeleteForParent<Security>(id,"SecurityRunID");
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
