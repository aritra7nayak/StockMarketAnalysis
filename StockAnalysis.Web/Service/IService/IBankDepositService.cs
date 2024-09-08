using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IBankDepositService
    {
        Task<ResponseDto?> GetUserBankDepositsAsync();
        Task<ResponseDto?> GetBankDepositByIdAsync(Guid id);
        Task<ResponseDto?> AddBankDepositAsync(BankDeposit bankDeposit);
        Task<ResponseDto?> UpdateBankDepositAsync(BankDeposit bankDeposit);
        Task<ResponseDto?> DeleteBankDepositAsync(Guid id);
    }
}
