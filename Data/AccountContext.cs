using Microsoft.EntityFrameworkCore;

namespace ATMApi.Data
{
    public class AccountContext: DbContext
    {
        public AccountContext (DbContextOptions<AccountContext> options)
            : base(options)
        { 
        }

        public DbSet<Account> Account { get; set; }
    }
}
