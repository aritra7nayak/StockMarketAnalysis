using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class BankDepositService: IBankDepositService
    {
        private readonly IBaseService _baseService;

        public BankDepositService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddBankDepositAsync(BankDeposit bankDeposit)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = bankDeposit,
                Url = SD.UserAnalytics + "/api/BankDeposit/AddBankDeposit"
            });
        }

        public async Task<ResponseDto?> DeleteBankDepositAsync(Guid id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.DELETE,
                Url = SD.UserAnalytics + "/api/BankDeposit/" + id
            });
        }

        public async Task<ResponseDto?> GetUserBankDepositsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/BankDeposit/GetBankDepositsByOwner"
            });
        }

        public async Task<ResponseDto?> GetBankDepositByIdAsync(Guid id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/BankDeposit/GetBankDepositById/" + id,
            });
        }
        public async Task<ResponseDto?> UpdateBankDepositAsync(BankDeposit bankDeposit)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = bankDeposit,
                Url = SD.UserAnalytics + "/api/BankDeposit/UpdateBankDeposit"
            });
        }

    }
}
