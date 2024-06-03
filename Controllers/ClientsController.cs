using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HomeBanking2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;



        public ClientsController(IClientRepository clientRepository)

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
                var client = _clientRepository.FindById(id);
                var clientDTO = new ClientDTO(client);
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
    }
}
