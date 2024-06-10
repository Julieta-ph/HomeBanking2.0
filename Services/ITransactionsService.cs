using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{
    public interface ITransactionsService
    {
        IEnumerable<Transaction> GetAllTransactions();

        Transaction GetTransactionById(long id);

        int SaveTransaction(Transaction transaction);

        TransferReturnDTO CreateTransaction(TransferDTO transferDTO, long id);

       

        

    }
}
