using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Services.IService;

namespace DataAcquisitionService.Services
{
    public class PriceService : IPriceService
    {
        private readonly IUnitofWork _unitOfWork;

        public PriceService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Price>> GetAllpriceRepositoryAsync()
        {
            return await _unitOfWork.priceRepository.GetAllAsync();
        }

        public async Task<Price> GetPriceByIdAsync(int id)
        {
            return await _unitOfWork.priceRepository.GetByIdAsync(id);
        }

        public async Task AddPriceAsync(Price price)
        {
            try
            {

                await _unitOfWork.priceRepository.AddAsync(price);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdatePriceAsync(Price price)
        {
            try
            {
                await _unitOfWork.priceRepository.UpdateAsync(price);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePriceAsync(int id)
        {
            var price = await _unitOfWork.priceRepository.GetByIdAsync(id);
            if (price != null)
            {
                await _unitOfWork.priceRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Price>> GetFilteredPriceAsync(string name)
        {
            //return await _unitOfWork.priceRepository.GetFilteredPriceAsync(name);
         throw new NotImplementedException();
        
        }

        public async Task<IEnumerable<Price>> GetAllPricesAsync()
        {
            return await _unitOfWork.priceRepository.GetAllAsync();
        }
    }
}