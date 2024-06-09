using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Repositories.Implementations;
using System.Security.Cryptography;

namespace HomeBanking2._0.Services.Implementations
{
    public class AccountService : IAccountService
    { 
        //Implementar los repositorios

        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        
        public IEnumerable<Account> GetAllAccountsByCliente(long clientId)
        {
            try
            {
                return _accountRepository.GetAccountsByClient(clientId);

            }
            catch (Exception)
            {
                throw new Exception("No se pudieron obtener todas las cuentas del cliente");
            }
        }

        public IEnumerable<Account> GetAllAccountsByClient()
        {
            throw new NotImplementedException();
        }

        public int GetCountAccountsByClient(long clientId)
        {
            var accountsByClient = _accountRepository.GetAccountsByClient(clientId);
            return accountsByClient.Count();
        }

        public string GetRandomAccountNumber()
        {
            string NumberAccountRandom;
        do
        {
            NumberAccountRandom = "VIN-" + RandomNumberGenerator.GetInt32(0, 99999999);

        } while (_accountRepository.GetAccountByNumber(NumberAccountRandom) != null);

            return NumberAccountRandom;

        }

        public void SaveAccount(Account account)
        {
            try
            {
                _accountRepository.SaveAccount(account);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo guardar la cuenta");

            }
        
        }
    }
}
