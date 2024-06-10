using HomeBanking2._0.Models;

namespace HomeBanking2._0.Repositories.Implementations
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        { 
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return FindAll()
                .ToList();
        }

        public Transaction GetTransactionById(long id)
        {
            return FindByCondition(transaction => transaction.Id == id)
                .FirstOrDefault();
        }

        public void SaveTransaction(Transaction transaction)
        {
            Create(transaction);
            SaveChanges();
        }
    }
}
