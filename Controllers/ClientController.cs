using HomeBanking2._0.DTOs;
using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeBanking2._0.Repositories.Implementations;
using HomeBanking2._0.Models;



namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

        }

     


        [HttpGet]

        public IActionResult GetAllClients()

        {
            try
            {
                var clients = _clientRepository.GetAllClients();
                var clientsDTO = clients.Select(c => new ClientDTO(c)).ToList();
                return Ok(clientsDTO);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetClientById(long id)

        {
            try
            {
                var client = _clientRepository.FindById(id);

                if (client == null)
                {

                    return Forbid();

                }

                var clientDTO = new ClientDTO(client);
                return Ok(clientDTO);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

   


        }



        

    }

}

