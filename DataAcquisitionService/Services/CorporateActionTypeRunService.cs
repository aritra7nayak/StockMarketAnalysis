using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.Importer;
using DataAcquisitionService.Services.IService;
using System.Data;

namespace DataAcquisitionService.Services
{
    public class CorporateActionTypeRunService: ICorporateActionTypeRunService
    {
        private readonly IUnitofWork _unitOfWork;

        public CorporateActionTypeRunService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCorporateActionTypeRunAsync(CorporateActionTypeRun corporateActionTypeRun)
        {
            try
            {

                await _unitOfWork.corporateActionTypeRunRepository.AddAsync(corporateActionTypeRun);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            try
            {
                switch (corporateActionTypeRun.SourceType)
                {
                    case SourceTypeEnum.NSE:
                        NSECorporateActionTypeImporter corporateActionTypeImporter = new NSECorporateActionTypeImporter(corporateActionTypeRun);
                        corporateActionTypeImporter.InitiateProcess();
                        DataTable dataTable = corporateActionTypeImporter.GetDataTable();
                        corporateActionTypeRun = await _unitOfWork.corporateActionTypeRunRepository.ProcessNSECorporateActionTypesAsync(corporateActionTypeRun, dataTable);
                        await _unitOfWork.corporateActionTypeRunRepository.UpdateAsync(corporateActionTypeRun);
                        await _unitOfWork.SaveChangesAsync();
                        break;

                }


            }
            catch (Exception ex)
            {

            }

        }


        public async Task DeleteCorporateActionTypeRunAsync(int id)
        {
            await _unitOfWork.corporateActionTypeRunRepository.DeleteForParent<CorporateActionType>(id, "CorporateActionTypeRunID");
        }

        public async Task<IEnumerable<CorporateActionTypeRun>> GetAllCorporateActionTypeRunsAsync()
        {
            return await _unitOfWork.corporateActionTypeRunRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CorporateActionTypeRun>> GetFilteredCorporateActionTypeRunsAsync(CorporateActionTypeRunFilterDto corporateActionTypeRunFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CorporateActionTypeRun> GetCorporateActionTypeRunByIdAsync(int id)
        {
            return await _unitOfWork.corporateActionTypeRunRepository.GetByIdAsync(id);
        }

        public Task UpdateCorporateActionTypeRunAsync(CorporateActionTypeRun corporateActionTypeRun)
        {
            throw new NotImplementedException();
        }
    }
}
