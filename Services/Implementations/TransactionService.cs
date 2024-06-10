using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Models;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using HomeBanking2._0.Repositories.Implementations;


namespace HomeBanking2._0.Services.Implementations
{
    public class TransactionService : ITransactionsService
    {
        private readonly ITransactionRepository _transactionsRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService) 
        {
            _transactionsRepository = transactionRepository;
            _accountService = accountService;
        }


        // Traer todas las transacciones

        public IEnumerable<Transaction> GetAllTransactions()
        {
            try
            {
                return _transactionsRepository.GetAllTransactions();
            }
            catch (Exception) 
            {
                throw new Exception("No fue posible traer las transacciones");
            }

        }

        //Traer la transaccion por ID
        public Transaction GetTransactionById(long id)
        {
            try
            {
                return _transactionsRepository.GetTransactionById(id);
            }
            catch (Exception)
            {
                throw new Exception("No fue posible traer la transacción");
            }
        }

     
        public int SaveTransaction(Transaction transaction)
        {
            try
            {
                /* return _transactionsRepository.SaveTransaction(transaction); */

                _transactionsRepository.SaveTransaction(transaction);
                return 1; // Indica que la transacción fue guardada con éxito
            }
            catch (Exception)
            {
                throw new Exception("No fue posible guardar la transacción");
            }
        }


        public TransferReturnDTO CreateTransaction(TransferDTO transferDTO, long clientId)
        {
            
            
                TransferReturnDTO TransfersToReturn = new();

            //Verificar que los parámetros no estén vacíos

            if (transferDTO.Amount == 0 || transferDTO.FromAccountNumber.IsNullOrEmpty() || transferDTO.ToAccountNumber.IsNullOrEmpty() || transferDTO.Description == "")
            {
                TransfersToReturn.MessageStatusCode = "Los datos no fueron ingresados correctamente.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            //Verificar que los números de cuenta no sean iguales

            if (transferDTO.FromAccountNumber.IsNullOrEmpty() || transferDTO.ToAccountNumber.IsNullOrEmpty() || transferDTO.ToAccountNumber == transferDTO.FromAccountNumber)
            {
                TransfersToReturn.MessageStatusCode = "Los datos de cuenta son iguales. Operacion Invalida.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            //Verificar que exista la cuenta de origen

            var fromAccount = _accountService.GetAccountByNumber(transferDTO.FromAccountNumber);
            if (fromAccount == null)
            {
                TransfersToReturn.MessageStatusCode = "La cuenta de origen no existe.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            //Verificar que la cuenta de origen pertenezca al cliente autenticado
            if (fromAccount.ClientId != clientId)
            {
                TransfersToReturn.MessageStatusCode = "La cuenta de origen no pertenece al cliente autenticado.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            //Verificar que exista la cuenta de destino
            var toAccount = _accountService.GetAccountByNumber(transferDTO.ToAccountNumber);
            if (toAccount == null)
            {
                TransfersToReturn.MessageStatusCode = "La cuenta de destino no existe.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            // Verificar que la cuenta de origen tenga el monto disponible.
            if (fromAccount.Balance < transferDTO.Amount)
            {
                TransfersToReturn.MessageStatusCode = "La cuenta no tiene monto disponible para realizar esta operacion.";
                TransfersToReturn.StatusCode = 403;
                TransfersToReturn.Transactions = null;
            }

            /*
             * Se deben crear dos transacciones,
             * -- Una con el tipo de transacción “DEBIT” asociada a la cuenta de origen
             * -- La otra con el tipo de transacción “CREDIT” asociada a la cuenta de destino.
            */

            var fromTransacction = new Transaction
            {
                Type = TransactionType.DEBIT.ToString(),
                Amount = -transferDTO.Amount,
                Description = transferDTO.FromAccountNumber + "-" + transferDTO.Description,
                Date = DateTime.Now,
                AccountId = fromAccount.Id
            };
            var toTransacction = new Transaction
            {
                Type = TransactionType.CREDIT.ToString(),
                Amount = transferDTO.Amount,
                Description = transferDTO.ToAccountNumber + "-" + transferDTO.Description,
                Date = DateTime.Now,
                AccountId = toAccount.Id

            };

            // se comprueba que la primera se haya ingresado a la DB
            if (SaveTransaction(fromTransacction) > 0)
            {
                if (SaveTransaction(toTransacction) > 0)
                {
                    //SaveTransaction(toTransacction);
                    

                    var fromTransactionInDB = GetTransactionById(fromTransacction.Id);
                    var toTransactionInDB = GetTransactionById(toTransacction.Id);
                    if (fromTransactionInDB != null && toTransactionInDB != null)
                    {
                        /*
                         * A la cuenta de origen se le restará el monto indicado en la petición
                         * a la cuenta de destino se le sumará el mismo monto.
                        */
                        var fromAccountDB = _accountService.GetAccountById(fromAccount.Id);
                        var toAccountDB = _accountService.GetAccountById(toAccount.Id);

                        if (fromAccountDB != null && toAccountDB != null)
                        {
                            fromAccountDB.Balance += fromTransacction.Amount;
                            toAccountDB.Balance += toTransacction.Amount;

                            //crear un nuevo metodo de UPDATEaCCOUNT
                            _accountService.UpdateAccount(fromAccountDB);
                            _accountService.UpdateAccount(toAccountDB);

                            TransfersToReturn.MessageStatusCode = "Las transacciones se crearon con exito y las cuentas fueron modificadas.";
                            TransfersToReturn.StatusCode = 201;
                            TransfersToReturn.Transactions = [new TransactionDTO(fromTransacction), new TransactionDTO(toTransacction)];
                        }
                        else
                        {
                            TransfersToReturn.MessageStatusCode = "Error en la actualizacion de cuentas.";
                            TransfersToReturn.StatusCode = 403;
                            TransfersToReturn.Transactions = null;
                        }
                    }
                    else
                    {
                        TransfersToReturn.MessageStatusCode = "Error en la carga de transacciones.";
                        TransfersToReturn.StatusCode = 500;
                        TransfersToReturn.Transactions = null;
                    }
                }
                // dentro de este ELSE se tendria que revertir la primera transaccion
                else
                {
                    TransfersToReturn.MessageStatusCode = "No se pudo cargar la transaccion de destino.";
                    TransfersToReturn.StatusCode = 500;
                    TransfersToReturn.Transactions = null;
                }
            }
            else
            {
                TransfersToReturn.MessageStatusCode = "No se pudo cargar la transaccion.";
                TransfersToReturn.StatusCode = 500;
                TransfersToReturn.Transactions = null;
            }
            return TransfersToReturn;
        }
    }
}
