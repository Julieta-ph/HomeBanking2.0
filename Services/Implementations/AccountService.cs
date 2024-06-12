using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Services;
using HomeBanking2._0.Repositories.Implementations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq.Expressions;
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

        

        public Account GetAccountById(long id)
        {
            try
            {
                return _accountRepository.GetAccountById(id);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener la cuenta a través del id");

            }
        }


        public IEnumerable<Account> GetAllAccountsByCliente(long clientId)
        {
            try
            {
                return _accountRepository.GetAccountsByClient(clientId);
            }
            catch (Exception)
            {
                throw new Exception("No se pudieron obtener las cuentas");
            }
        }

        public Account GetAccountByNumber(string numberAccount)
        {
            try
            {
                return _accountRepository.GetAccountByNumber(numberAccount);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener la cuenta");

            }
        }

        public Account SaveAccount(long newIdCreated)
        {
            try
            {
                Account account = new Account
                {
                    Number = GetRandomAccountNumber().ToString(),
                    Balance = 0,
                    CreationDate = DateTime.Now,
                    ClientId = newIdCreated
                };

                _accountRepository.SaveAccount(account);

                return _accountRepository.GetAccountByNumber(account.Number);
            }
            catch (Exception) 
            {
                throw new Exception("No fue posible guardar la cuenta");
            }
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                _accountRepository.SaveAccount(account);
            }
            catch (Exception)
            {
                throw new Exception("No fue posible modificar la cuenta");
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            try
            {
                return _accountRepository.GetAllAccounts();
            }
            catch (Exception)
            {
                throw new Exception("No se pudieron traer todas las cuentas");
            }
        }

        

    }
}
