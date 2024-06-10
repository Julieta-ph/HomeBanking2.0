using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{

    //Generar interfaz de servicios 

    public interface IAccountService
    {
        string GetRandomAccountNumber(); 

        int GetCountAccountsByClient(long clientId); 

        Account SaveAccount(long newIdCreated);

        Account GetAccountByNumber(string numberAccount);
        
        void UpdateAccount(Account account); 

        IEnumerable<Account> GetAllAccountsByCliente(long clientId); 

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountById(long id);

    }
}
