using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Repositories.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //responde la solicitud http de los clientes, que pueden ser web por ejemplo

        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }

        // GET: api/Accounts

        [HttpGet]
        public ActionResult<IEnumerable<AccountDTO>> GetAccounts()
        {


            try
            {
                var accounts = _accountRepository.GetAllAccounts();
                var accountsDTO = accounts.Select(a => new AccountDTO(a)).ToList();
                return Ok(accountsDTO);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetAccountById(long id)

        {
            try
            {
                var account = _accountRepository.FindById(id);
                var accountDTO = new AccountDTO(account);
                return Ok(accountDTO);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }




        }

        

    }

}


       

