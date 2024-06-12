using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;

namespace HomeBanking2._0.Services
{
    public interface ITransactionsService
    {
        IEnumerable<Transaction> GetAllTransactions();

        Transaction GetTransactionById(long id);

        int SaveTransaction(Transaction transaction);


        List<Transaction> GetTransactionByIdList(long id);

        


    }
}
