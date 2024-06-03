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

        

        

        public void Save(Account Account)
        {
            throw new NotImplementedException();
        }

        
    }
}
