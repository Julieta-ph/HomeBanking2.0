using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAccountService _accountService;
        private readonly ITransactionsService _transactionService;

        private readonly IAccountRepository _accountRepository;

        //crear constructor con todos los repositorios que voy a usar
        public TransactionsController(

            IClientService clientService,
            IAccountService accountService,
            ITransactionsService transactionService,

            IAccountRepository accountRepository

        )
        {
            _clientService = clientService;
            _accountService = accountService;
            _transactionService = transactionService;

            _accountRepository = accountRepository;
        }


        /*
        
            Debe recibir el monto, la descripción, número de cuenta de origen y número de cuenta de destino como parámetros de solicitud
            Verificar que los parámetros no estén vacíos
            
            
            Verificar que la cuenta de origen pertenezca al cliente autenticado
          
           
            
         */

       

            [HttpPost]
            [Authorize(Policy = "ClientOnly")]
            public IActionResult Post([FromBody] TransferDTO transferDTO)

            {
                try
                {
                    string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                    if (email == string.Empty)
                    {
                        return StatusCode(403, "El usuario no pudo ser encontrado");
                    }
                    Client client = _clientService.GetClientByEmail(email);
                    if (client == null)
                    {
                        return StatusCode(403, "La cuenta del cliente no existe");
                    }

                    // Verificar que los números de cuenta no sean iguales

                    if (transferDTO.ToAccountNumber == transferDTO.FromAccountNumber)
                    {
                        return StatusCode(403, "El número de cuenta tanto de origen como de destino, no puede ser el mismo");
                    }

                    //Verificar que exista la cuenta de origen y destino

                    if (transferDTO.FromAccountNumber == string.Empty || transferDTO.ToAccountNumber == string.Empty)
                    {
                        return StatusCode(403, "No fue posible localizar la cuenta");
                    }

                    // Verificar que la cuenta de origen tenga el monto disponible.

                    if (transferDTO.Amount <= 0)
                    {
                        return StatusCode(403, "Es necesario tener saldo disponible para realizar una operación");
                    }

                    // Description no puede estar vacio

                    if (transferDTO.Description == string.Empty)
                    {
                        return StatusCode(403, "Es necesario colocar el motivo de la operación para poder realizar una operación");
                    }


                    // traer cuenta ORIGEN y verificar que exista

                    Account fromAccount = _accountService.GetAccountByNumber(transferDTO.FromAccountNumber);
                    if (fromAccount == null)
                    {
                        return StatusCode(403, "La cuenta de origen no existe");
                    }

                    // verifico que el monto de la transaccion sea menor o igual al monto de la transferencia

                    if (fromAccount.Balance <= transferDTO.Amount)
                    {
                        return StatusCode(403, "Fondos insuficientes para realizar la transferencia");
                    }

                    // traer cuenta DESTINO y verificar que exista

                    Account toAccount = _accountService.GetAccountByNumber(transferDTO.ToAccountNumber);
                    if (toAccount == null)
                    {
                        return StatusCode(403, "La cuenta de destino no existe");
                    }

                    // Se deben crear dos transacciones, una con el tipo de transacción “DEBIT” asociada a la cuenta de origen y la otra con el tipo de transacción “CREDIT” asociada a la cuenta de destino.
                    // A la cuenta de origen se le restará el monto indicado en la petición y a la cuenta de destino se le sumará el mismo monto.


                    _transactionService.SaveTransaction(new Transaction
                    {
                        Type = TransactionType.DEBIT.ToString(),
                        Amount = transferDTO.Amount * -1,
                        Description = transferDTO.Description + " " + toAccount.Number,
                        AccountId = fromAccount.Id,
                        Date = DateTime.Now,
                    });

                    _transactionService.SaveTransaction(new Transaction
                    {
                        Type = TransactionType.CREDIT.ToString(),
                        Amount = transferDTO.Amount,
                        Description = transferDTO.Description + " " + fromAccount.Number,
                        AccountId = toAccount.Id,
                        Date = DateTime.Now,

                    });

                    fromAccount.Balance = fromAccount.Balance - transferDTO.Amount;

                    _accountRepository.UpdateAccount(fromAccount);

                    toAccount.Balance = toAccount.Balance + transferDTO.Amount;

                    _accountRepository.UpdateAccount(toAccount);


                    return Ok();

                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);

                }
            }






        }



    }



