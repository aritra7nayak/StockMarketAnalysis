using UserAnalyticsService.Models;
using UserAnalyticsService.Repository;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Service
{
    public class BankDepositService
    {
        private readonly IBankDepositRepository _bankDepositRepository;

        public BankDepositService(IBankDepositRepository bankDepositRepository)
        {
            _bankDepositRepository = bankDepositRepository;
        }

        public async Task<IEnumerable<BankDeposit>> GetAllBankDeposits()
        {
            return await _bankDepositRepository.GetAll();
        }

        // Retrieve a specific bankDeposit by ID
        public async Task<BankDeposit> GetBankDepositById(Guid id, string userId)
        {
            var result = await _bankDepositRepository.Find(p => p.Owner == userId && p.Id == id);
            return result.FirstOrDefault();
        }

        // Add a new bankDeposit
        public async Task AddBankDeposit(BankDeposit bankDeposit)
        {
            if (bankDeposit.InvestmentDetails != null)
            {
                foreach (var detail in bankDeposit.InvestmentDetails)
                {
                    detail.UpdateMaturityDetails();
                }
            }

            bankDeposit.UpdateValues();
            await _bankDepositRepository.Add(bankDeposit);
        }

        // Update an existing bankDeposit
        public async Task<bool> UpdateBankDeposit(BankDeposit bankDeposit)
        {
            var result = await _bankDepositRepository.Find(p => p.Owner == bankDeposit.Owner && p.Id == bankDeposit.Id);
            if (result != null)
            {
                if (bankDeposit.InvestmentDetails != null)
                {
                    foreach (var detail in bankDeposit.InvestmentDetails)
                    {
                        detail.UpdateMaturityDetails();
                    }
                }
                bankDeposit.UpdateValues();
                return await _bankDepositRepository.Update(bankDeposit);

            }
            else
            {
                return false;
            }
        }

        // Delete a bankDeposit by ID
        public async Task<bool> DeleteBankDeposit(Guid id)
        {
            return await _bankDepositRepository.Delete(id);
        }

        // Example of a specific method that might be unique to BankDeposit
        public async Task<IEnumerable<BankDeposit>> GetBankDepositsByOwner(string name)
        {
            return await _bankDepositRepository.Find(p => p.Owner == name);
        }

    }
}
