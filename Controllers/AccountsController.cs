﻿using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Repositories.Implementations;
using HomeBanking2._0.Services;
using HomeBanking2._0.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IAccountService _accountService;
        private readonly ITransactionsService _transactionService;

        public AccountsController(

            IAccountService accountService,
            ITransactionsService transactionService
        )

        {

            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetAccount(long id)
        {
            try
            {
                var account = _accountService.GetAccountById(id);
                var cuentaId = account.Id;
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


       
