using ATMApi.Models;

namespace ATMApi.Repository
{
    public interface IAccountRepository
    {
        Task<List<AccountModels>> GetAllAccountsAsync();
        Task<AccountModels> GetAccountByIdAsync(int id);
        Task<int> NewAccountAsync(AccountModels accountmodel);
        Task UpdateAccountAsync(int accountId, AccountModels acountmodels);
        Task UpdateAccountPatchAsync(int accountId, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument accountmodels);
        Task DeleteAccountAsync(int accountid);
    }
}
