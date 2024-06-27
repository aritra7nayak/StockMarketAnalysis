using DataAcquisitionService.Dtos.FilterDto;
using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;
using System.Data;

namespace DataAcquisitionService.Services
{
    public class PriceRunService : IPriceRunService
    {
        private readonly IUnitofWork _unitOfWork;

        public PriceRunService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPriceRunAsync(PriceRun priceRun)
        {
            try
            {

                await _unitOfWork.priceRunRepository.AddAsync(priceRun);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            try
            {
                switch (priceRun.SourceType)
                {
                    //case SourceTypeEnum.NSE:
                    //    NSEPriceImporter priceImporter = new NSEPriceImporter(priceRun);
                    //    priceImporter.InitiateProcess();
                    //    DataTable dataTable = priceImporter.GetDataTable();
                    //    priceRun = await _unitOfWork.priceRunRepository.ProcessNSEPricesAsync(priceRun, dataTable);
                    //    await _unitOfWork.priceRunRepository.UpdateAsync(priceRun);
                    //    await _unitOfWork.SaveChangesAsync();
                    //    break;
                    //case SourceTypeEnum.BSE:
                    //    BSEPriceImporter bsePriceImporter = new BSEPriceImporter(priceRun);
                    //    bsePriceImporter.InitiateProcess();
                    //    DataTable bsedataTable = bsePriceImporter.GetDataTable();
                    //    priceRun = await _unitOfWork.priceRunRepository.ProcessBSEPricesAsync(priceRun, bsedataTable);
                    //    await _unitOfWork.priceRunRepository.UpdateAsync(priceRun);
                    //    await _unitOfWork.SaveChangesAsync();
                    //    break;

                }


            }
            catch (Exception ex)
            {

            }

        }


        public async Task DeletePriceRunAsync(int id)
        {
            await _unitOfWork.priceRunRepository.DeleteForParent<Price>(id, "PriceRunID");
        }

        public async Task<IEnumerable<PriceRun>> GetAllPriceRunsAsync()
        {
            return await _unitOfWork.priceRunRepository.GetAllAsync();
        }

        public async Task<IEnumerable<PriceRun>> GetFilteredPriceRunsAsync(PriceRunFilterDto priceRunFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<PriceRun> GetPriceRunByIdAsync(int id)
        {
            return await _unitOfWork.priceRunRepository.GetByIdAsync(id);
        }

        public Task UpdatePriceRunAsync(PriceRun priceRun)
        {
            throw new NotImplementedException();
        }
    }
}
