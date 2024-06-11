using HomeBanking2._0.Models;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace HomeBanking2._0.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        
        Account FindById(long id);

        IEnumerable<Account> GetAccountsByClient(long clientId);

        Account GetAccountById(long id);

        Account GetAccountByNumber(string numberAccount);

        IEnumerable<Account> GetAllAccountsByClient(long clientId);

        Account FindByNumber(string numberAccount);

        void Save(Account account);

        void UpdateAccount(Account account);

    }
}
