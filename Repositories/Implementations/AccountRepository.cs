using HomeBanking2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Repositories.Implementations
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Account FindById(long id)
        {
            return FindByCondition(a => a.Id == id)
                .Include(a => a.Transactions)
                .FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                .Include(a => a.Transactions)
                .ToList();
        }

        public IEnumerable<Account> GetAccountsByClient(long clientId)
        {
            return FindByCondition(account => account.ClientId == clientId)

            .Include(account => account.Transactions)
            .ToList();

        }

        public Account GetAccountById(long id)
        {
            return FindByCondition(account => account.Id == id)
                .Include(account => account.Transactions)
                .FirstOrDefault();
        }

        public Account GetAccountByNumber(string numberAccount)
        {
            return FindByCondition(account => account.Number == numberAccount)
                .Include(account => account.Transactions)
                .FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccountsByClient(long clientId)
        {
            return FindByCondition(a => a.ClientId == clientId)
                .Include(a => a.Transactions)
                .ToList();

        }
     

        public void SaveAccount(Account account)
        {
            Create(account);
            SaveChanges();
        }
    }
    
    
}
