using HomeBanking2._0.Models;
using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using HomeBanking2._0.Services;

namespace HomeBanking2._0.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientLoanRepository _clientLoanRepository;

        private readonly IClientService _clientService;

        public LoansController(
            IAccountRepository accountRepository,
            IClientLoanRepository clientLoanRepository,
            IClientRepository clientRepository,
            ILoanRepository loanRepository,
            ITransactionRepository transactionRepository,

            IClientService clientService
            
            )
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _loanRepository = loanRepository;
            _transactionRepository = transactionRepository;
            _clientLoanRepository = clientLoanRepository;

            _clientService = clientService;
        }

       
           
        [HttpGet]
        public IActionResult GetLoans()
        {
            try
            {
                
                var loans = _loanRepository.GetAllLoans();

                var loansDTO = loans.Select(lc => new LoanDTO(lc)).ToList();
                
                return Ok(loansDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

            //Debe recibir un objeto de solicitud de crédito con los datos del préstamo
            
            //Se debe crear una transacción “CREDIT” asociada a la cuenta de destino(el monto debe quedar positivo) con la descripción concatenando el nombre del préstamo y la frase “loan approved”
            //Se debe actualizar la cuenta de destino sumando el monto solicitado.

        [HttpPost]
        public IActionResult Post([FromBody] LoanAplicationDTO loanAppDTO)
        {
            try
            {      //UTILIZAMOS EL OBJETO USER PARA LOCALIZAR UN CLIENTE AUTENTIFICADO UTILIZANDO LAS CLAIM
                   //SI SE LOCALIZA EL USUARIO CON EL MAIL(QUE ES LA CLAIM EN ESTE CASO), ESTA OK

                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;

                Client client = _clientRepository.FindByEmail(email);
                if (client == null)
                {
                    return StatusCode(403, "El usuario no pudo ser encontrado");
                }

                //Se verifica que el monto del prestamo no sea 0 o menos

                if (loanAppDTO.Amount <= 0)
                {
                    return StatusCode(403, "El monto debe ser mayor a $0");
                }

                //traemos el objeto loan desde el repo utilizando el loanID, a traves de la inyeccion de dependencias                
                //Verificamos que el préstamo exista

                Loan loan = _loanRepository.FindById(loanAppDTO.LoanId);
                if (loan == null)
                {
                    return StatusCode(403, "El prestamo que usted quiere solicitar no existe");
                }

                //Verificamos que el monto solicitado no exceda el monto máximo del préstamo

                if (loanAppDTO.Amount > loan.MaxAmount)
                {
                    return StatusCode(403, "El prestamo solicitado excede el monto permitido");
                }

                //Verifica que la cantidad de cuotas se encuentre entre las disponibles del préstamo

                if (loanAppDTO.Payments.IsNullOrEmpty())
                {
                    return StatusCode(403, "Es necesario seleccionar la cantidad de cuotas a utilizar");
                }

                //Extraemos y limpiamos los valores de pago del objeto loan.
                //Comprobamos si el valor de pago proporcionado por el usuario se encuentra entre las opciones válidas.
                // tomamos los valores de pago y los almacenamos en una nueva lista
                var newPaymentValues = loan.Payments.Split(',').Select(s => s.Trim()).ToList();
                if (!newPaymentValues.Contains(loanAppDTO.Payments.ToString())) //verificamos que el valor del pago sea valido
                {
                    return BadRequest("La cantidad de cuentas ingresadas no es valida");
                }

                //buscamos la cuenta destino en nuestro repo y corroboramos que exista
                Account toAccount = _accountRepository.FindByNumber(loanAppDTO.ToAccountNumber);
                if (toAccount == null)
                {
                    return StatusCode(403, "La cuenta no existe");
                }

                //Verificar que la cuenta de destino pertenezca al cliente autenticado
                if (toAccount.ClientId != client.Id)
                {
                    return StatusCode(403, "La cuenta no pertenece al cliente");
                }

                //Se debe crear una solicitud de préstamo con el monto solicitado sumando el 20% del mismo
                //calculamos el prestamo y se lo asignamos al cliente

                double finalAmount = loanAppDTO.Amount * 1.2;
                _clientLoanRepository.Save(new ClientLoan
                {
                    ClientId = toAccount.ClientId,
                    Amount = finalAmount,
                    Payments = loanAppDTO.Payments,
                    LoanId = loanAppDTO.LoanId,
                });

                _transactionRepository.SaveTransaction(new Transaction
                {
                    Type = TransactionType.CREDIT.ToString(),
                    Amount = loanAppDTO.Amount,
                    Description = "Loan approved " + loan.Name,
                    AccountId = toAccount.Id,
                    Date = DateTime.Now,

                });


                toAccount.Balance = toAccount.Balance + loanAppDTO.Amount;

                _accountRepository.Save(toAccount);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
