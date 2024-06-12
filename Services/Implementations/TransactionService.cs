using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Models;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using HomeBanking2._0.Repositories.Implementations;


namespace HomeBanking2._0.Services.Implementations
{
    public class TransactionService : ITransactionsService
    {
        private readonly ITransactionRepository _transactionsRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService) 
        {
            _transactionsRepository = transactionRepository;
            _accountService = accountService;
        }


        // Traer todas las transacciones

        public IEnumerable<Transaction> GetAllTransactions()
        {
            try
            {
                return _transactionsRepository.GetAllTransactions();
            }
            catch (Exception) 
            {
                throw new Exception("No fue posible traer las transacciones");
            }

        }

        //Traer la transaccion por ID
        public Transaction GetTransactionById(long id)
        {
            try
            {
                return _transactionsRepository.GetTransactionById(id);
            }
            catch (Exception)
            {
                throw new Exception("No fue posible traer la transacción");
            }
        }

     
        public int SaveTransaction(Transaction transaction)
        {
            try
            {
                /* return _transactionsRepository.SaveTransaction(transaction); */

                _transactionsRepository.SaveTransaction(transaction);
                return 1; // Indica que la transacción fue guardada con éxito
            }
            catch (Exception)
            {
                throw new Exception("No fue posible guardar la transacción");
            }
        }

        public List<Transaction> GetTransactionByIdList(long id)
        {

            var transactionList = _transactionsRepository.GetTransactionByIdList(id);
            return transactionList.ToList();

        }


    }
}
