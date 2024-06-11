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
        private readonly ITransactionRepository transactionRepository;
        private readonly IClientLoanRepository clientLoanRepository;

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

        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var loans = _loanRepository.GetAllLoans();
                var LoansDTO = new List<ClientLoanDTO>();
                foreach (ClientLoan clientloan in loans)
                {
                    var newLoanDTO = new ClientLoanDTO

                    {
                        Id = clientloan.Id,
                        LoanId = clientloan.Id,
                        Name = clientloan.Name,
                        Amount = clientloan.Amount,
                        Payments = Convert.ToInt32(clientloan.Payments),
                        
                    };
                    LoansDTO.Add(newLoanDTO);
                }
                return StatusCode(200, ClientLoanDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        
    }
}
