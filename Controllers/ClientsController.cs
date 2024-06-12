using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using HomeBanking2._0.Repositories.Implementations;
using HomeBanking2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.Linq.Expressions;


namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientsController : ControllerBase
    {
        // implementar repositorios
        // eliminamos repositorios y lo reemplazamos por servicios
        //

        /*
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;

        */



        //implementar servicios

        private readonly IAccountService _accountService;
        private readonly IClientService _clientService;
        private readonly ICardService _cardService;


        //reemplazamos repositorios por servicios

        /*
        
        public ClientsController(
                IClientRepository clientRepository,
                IAccountRepository accountRepository,
                ICardRepository cardRepository
            
        );
        
        */


        public ClientsController(

            IClientService clientService,
            IAccountService accountService,
            ICardService cardService

        )

        {
            _cardService = cardService;
            _clientService = clientService;
            _accountService = accountService;
        }

        public Client GetCurrentClient()
        {
            /*
                _clientRepository = clientRepository;
                _accountRepository = accountRepository;
                _cardRepository = cardRepository;

            */

            string email = User.FindFirst("Client")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Usuario no encontrado");
            }

            Client client = _clientService.GetClientByEmail(email);

            return client;
        }

        

        [HttpGet]
      //  [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAllClients()
        {
            try
            {
                /*
                var clients = _clientRepository.GetAllClients();
                */

                var clientsDTO = _clientService.GetAllClients();
                return Ok(clientsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{id}")]

        public IActionResult GetClientById(long id)
        {
            try
            {
                /*
                var client = _clientRepository.FindById(id);
                */

                var clientByIddto = _clientService.GetClientById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("current")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCurrent()
        {
            /*
             * Reemplazamos por servicios
            try
            {

                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : null;
                if (email.IsNullOrEmpty())
                    return StatusCode(403, "Usuario no encontrado");

                Client client = _clientRepository.FindByEmail(email);
                if (client == null)
                    return StatusCode(403, "Usuario no encontrado");

                return Ok(new ClientDTO(client));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            */

            try
            {

                Client clientCurrent = GetCurrentClient();
               
                return Ok(new ClientDTO(clientCurrent));
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }


        }

        [HttpPost]

        public IActionResult Post([FromBody] NewClientDTO newClientDTO)
        {
            try
            {


                Client client = _clientService.GetClientByEmail(newClientDTO.Email);
                if (client != null)
                {
                    return StatusCode(403, "El email ya está en uso");
                }

                //GUARDO CLIENTE Y RETORNO ID

                Client ClientCreated = _clientService.Save(newClientDTO);

                //crear cuenta usando service

                _accountService.SaveAccount(ClientCreated.Id);
                ClientDTO ClientDto = new ClientDTO(ClientCreated);

                return Ok();


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("current/accounts")]
        [Authorize(Policy = "ClientOnly")]

        //creo cuenta con el cliente autenticado

        public IActionResult CreateAccountToClientAuthenticated()
        {
            try
            {
                //traigo todas las cuentas para verificar que sean menos de 3

                Client clientCurrent = GetCurrentClient(); //REVISAR


                if (_accountService.GetCountAccountsByClient(clientCurrent.Id) < 3)
                {
                    Account accountCreated = _accountService.SaveAccount(clientCurrent.Id);
                    return StatusCode(201, "La cuenta se creó correctamente");
                }
                else
                {
                    return StatusCode(403, "No es posible tener más de 3 cuentas");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("current/accounts")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetAllAccountsByClient()
        {
            try
            {
                //traemos todas las cuentas del cliente autenticado

                Client clientCurrent = GetCurrentClient();

                var accountsByClient = _accountService.GetAllAccountsByCliente(clientCurrent.Id);

                var accountsDTO = accountsByClient.Select(a => new AccountClientDTO(a)).ToList();

                return Ok(accountsDTO);



            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }



        }


        // traer el cliente autenticado, confirmar que tiene menos de 6 tarjetas.
        // Si esta OK, crear tarjeta y asignarla. Los numeros de tarjeta y cvv deben ser aleatorios

        [HttpPost("current/cards")]
        [Authorize(Policy = "ClientOnly")]

        public IActionResult CreateCardToClientAuthenticated([FromBody] NewCardDTO newCardDTO)
        {
            try
            {
                Client clientCurrent = GetCurrentClient();

                var cardByClient = _cardService.GetAllCardsByType(clientCurrent.Id, newCardDTO.type);

                if (cardByClient.Count() < 3)
                {
                    if (!cardByClient.Any(c => c.Color == newCardDTO.color))
                    {
                        var newCard = new Card
                        {
                            ClientId = clientCurrent.Id,
                            CardHolder = clientCurrent.FirstName + " " + clientCurrent.LastName,
                            Color = newCardDTO.color,
                            Cvv = _cardService.GenerateCVV(),
                            FromDate = DateTime.Now,
                            ThruDate = DateTime.Now.AddYears(5),
                            Number = _cardService.GenerateNumberCard(clientCurrent.Id),
                            Type = newCardDTO.type,
                        };

                        _cardService.AddCard(newCard);


                    }
                    else
                    {
                        return StatusCode(403, "No es posible crear otra tarjeta, llegaste al límite disponible");
                    }



                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }





    }
}
