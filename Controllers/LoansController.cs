using HomeBanking2._0.Models;
using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;

namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientLoanRepository _clientLoanRepository;

        public LoansController(
            IAccountRepository accountRepository,
            IClientLoanRepository clientLoanRepository,
            IClientRepository clientRepository,
            ILoanRepository loanRepository,
            ITransactionRepository transactionRepository
            
            )
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _loanRepository = loanRepository;
            _transactionRepository = transactionRepository;
            _clientLoanRepository = clientLoanRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var loans = _loanRepository.GetAllLoans();
                var ClientLoan = new List<ClientLoanDTO>();
                foreach (ClientLoan clientloan in loans)
                {
                    var newLoanDTO = new ClientLoanDTO

                    {
                        Id = clientloan.Id,
                        LoanId = clientloan.Id,
                        Name = clientloan.Loan.Name,
                        Amount = clientloan.Amount,
                        Payments = Convert.ToInt32(clientloan.Payments),
                        
                    };
                    ClientLoan.Add(newLoanDTO);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [Http]
        
    }
}
