using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.Importer;
using DataAcquisitionService.Services.IService;
using System.Data;

namespace DataAcquisitionService.Services
{
    public class CorporateActionRunService:ICorporateActionRunService
    {
        private readonly IUnitofWork _unitOfWork;

        public CorporateActionRunService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCorporateActionRunAsync(CorporateActionRun corporateActionRun)
        {
            try
            {

                await _unitOfWork.corporateActionRunRepository.AddAsync(corporateActionRun);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            try
            {
                switch (corporateActionRun.SourceType)
                {
                    case SourceTypeEnum.NSE:
                        NSECorporateActionImporter corporateActionImporter = new NSECorporateActionImporter(corporateActionRun);
                        corporateActionImporter.InitiateProcess();
                        DataTable dataTable = corporateActionImporter.GetDataTable();
                        corporateActionRun = await _unitOfWork.corporateActionRunRepository.ProcessNSECorporateActionsAsync(corporateActionRun, dataTable);
                        await _unitOfWork.corporateActionRunRepository.UpdateAsync(corporateActionRun);
                        await _unitOfWork.SaveChangesAsync();
                        break;

                }


            }
            catch (Exception ex)
            {

            }

        }


        public async Task DeleteCorporateActionRunAsync(int id)
        {
            await _unitOfWork.corporateActionRunRepository.DeleteForParent<CorporateAction>(id, "CorporateActionRunID");
        }

        public async Task<IEnumerable<CorporateActionRun>> GetAllCorporateActionRunsAsync()
        {
            return await _unitOfWork.corporateActionRunRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CorporateActionRun>> GetFilteredCorporateActionRunsAsync(CorporateActionRunFilterDto corporateActionRunFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CorporateActionRun> GetCorporateActionRunByIdAsync(int id)
        {
            return await _unitOfWork.corporateActionRunRepository.GetByIdAsync(id);
        }

        public Task UpdateCorporateActionRunAsync(CorporateActionRun corporateActionRun)
        {
            throw new NotImplementedException();
        }
    }
}