using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {

        private readonly IClientService _clientService;
        private readonly ICardService _cardService;


        public CardsController(

            IClientService clientService,          
            ICardService cardService

        )

        {
            _cardService = cardService;
            _clientService = clientService;         
        }

        public Client GetCurrentClient()
        {

            string email = User.FindFirst("Client")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Usuario no encontrado");
            }

            Client client = _clientService.GetClientByEmail(email);

            return client;
        }


        

        //traer el cliente autenticado y todas sus tarjetas

        [HttpGet("current/cards")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetAllCardsByClient()
        {
            try

            {
                Client clientCurrent = (Client)GetCurrentClient(); 

                var cardsByClient = _cardService.GetAllCardsByClient(clientCurrent.Id);

                var cardDTO = cardsByClient.Select(c => new CardDTO(c)).ToList();

                return Ok(cardDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }




        }

    }
}
