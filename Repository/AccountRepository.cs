using ATMApi.Data;
using ATMApi.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace ATMApi.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository (AccountContext context)
        {
            _context = context;
        }

        public async Task<List<AccountModels>> GetAllAccountsAsync()
        {
            var accounts = await _context.Account.Select(x => new AccountModels()
            {
                Id = x.Id,
                Name = x.Name,
                Balance = x.Balance,
            }).ToListAsync();

            return accounts;
        }

        public async Task<AccountModels> GetAccountByIdAsync(int id)
        {
            var account = await _context.Account.Where(x=>x.Id == id).Select(x => new AccountModels()
            {
                Id = x.Id,
                Name = x.Name,
                Balance = x.Balance
            }).FirstOrDefaultAsync();

            return account;
        }

        public async Task<int> NewAccountAsync(AccountModels accountmodel)
        {
            var account = new Account()
            {
                Name = accountmodel.Name,
                Balance = accountmodel.Balance
            };

            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return account.Id;
        }

        public async Task UpdateAccountAsync(int  accountId, AccountModels accountmodels)
        {
            var account = await _context.Account.FindAsync(accountId);
            if (account == null)
            {
                return;
            }
            account.Name = accountmodels.Name;
            account.Balance = accountmodels.Balance;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountPatchAsync(int accountId, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument accountmodels)
        {
            var account = await _context.Account.FindAsync(accountId);
            if (account == null)
            {
                return;
            }
            accountmodels.ApplyTo(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountid)
        {
            var account = new Account() { Id = accountid };
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}
