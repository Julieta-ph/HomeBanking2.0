using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Repositories.Implementations
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(HomeBankingContext repositoryContext) : base(repositoryContext) { }

        public Loan FindById(long id)
        {
            return FindByCondition(Loan => Loan.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return FindAll();
        }

        

       
    }
}
