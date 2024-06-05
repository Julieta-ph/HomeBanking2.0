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
        public IActionResult NewClient([FromBody]NewClientDTO newClientDTO)
        {
            try
            {
                if (newClientDTO.FirstName.IsNullOrEmpty() || newClientDTO.LastName.IsNullOrEmpty() || newClientDTO.Email.IsNullOrEmpty() || newClientDTO.Password.IsNullOrEmpty())
                    return StatusCode(StatusCodes.Status400BadRequest, "Información no encontrada");
                Client client = _clientRepository.FindByEmail(newClientDTO.Email);
                if (client != null)
                    return StatusCode(StatusCodes.Status400BadRequest, "El email ya fue utilizado");
                Client newClient = new Client
                {
                    FirstName = newClientDTO.FirstName,
                    LastName = newClientDTO.LastName,
                    Email = newClientDTO.Email,
                    Password = newClientDTO.Password,

                }; //guardar el nuevo cliente en la base de datos

                _clientRepository.Save(newClient);
                return StatusCode(201, new ClientDTO(newClientDTO));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
