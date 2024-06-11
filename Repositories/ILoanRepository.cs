using HomeBanking2._0.Models;

namespace HomeBanking2._0.Repositories
{
    public interface ILoanRepository
    {
        public IEnumerable<Loan> GetAllLoans();

        public Loan FindById(long id);

    }
}
