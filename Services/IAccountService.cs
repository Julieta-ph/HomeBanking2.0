using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{

    //Generar interfaz de servicios 

    public interface IAccountService
    {
        string GetRandomAccountNumber();
        int GetCountAccountsByClient(long clientId);

        void SaveAccount(Account account);

        IEnumerable<Account> GetAllAccountsByClients();

    }
}
