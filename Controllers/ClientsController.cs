using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


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

        [HttpGet("current")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCurrentClient()
        {
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
        }

        [HttpPost]

        public IActionResult ClientDTO(Client clientDTO)
        {
            try
            {
                if (clientDTO.FirstName.IsNullOrEmpty() || clientDTO.LastName.IsNullOrEmpty() || clientDTO.Email.IsNullOrEmpty() || clientDTO.Password.IsNullOrEmpty())
                return StatusCode(StatusCodes.Status400BadRequest, "Datos inválidos");

                Client client = _clientRepository.FindByEmail(clientDTO.Email);

                if (client != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El email indicado está en uso");
                }

                Client newClientDTO = new Client
                {
                    FirstName = clientDTO.FirstName,
                    LastName = clientDTO.LastName,
                    Email = clientDTO.Email,
                    Password = clientDTO.Password,
                };

                _clientRepository.Save(newClientDTO);
                return Created("Usuario creado", newClientDTO);

            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        

    }
}
