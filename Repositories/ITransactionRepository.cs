using HomeBanking2._0.Models;
//using System.Transactions;

namespace HomeBanking2._0.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();

        Transaction GetTransactionById(long id);


        void SaveTransaction(Transaction transaction);

        IQueryable<Transaction> GetTransactionByIdList(long id);

    }
}
