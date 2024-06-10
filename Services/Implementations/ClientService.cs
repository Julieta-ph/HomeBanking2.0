using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using System.Net;

namespace HomeBanking2._0.Services.Implementations
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        { 
            _clientRepository = clientRepository;
        }

        //traemos clientes desde el repositorio de Clients segun metodo y en caso que no se pueda, devolvemos el error que corresponda segun el caso
    
       

        public Client GetClientByEmail(string email)
        {
            try
            {
                return _clientRepository.FindByEmail(email);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener cliente a través del email");

            }
          
        }

        

        public long SaveAndReturnIdClient(Client client)
        {
            try
            {
                _clientRepository.Save(client);
                Client newClientSave = _clientRepository.FindByEmail(client.Email);
                return newClientSave.Id;

            }
            catch (Exception)
            {

                throw new Exception("No se pudieron guardar y obtener los clientes");
            }
        }

       

        public Client Save(NewClientDTO newClientDTO)
        {
            try
            {
                Client client = new Client
                {
                    Email = newClientDTO.Email,
                    Password = newClientDTO.Password,
                    FirstName = newClientDTO.FirstName,
                    LastName = newClientDTO.LastName,
                };
                _clientRepository.Save(client);

                return _clientRepository.FindByEmail(client.Email);
            }
            catch (Exception)
            {
                throw new Exception("No fue posible guardar y traer al cliente");
            }
        }

        public ClientDTO GetClientById(long id)
        {
            try
            {
                Client clientById = _clientRepository.FindById(id);
                return new ClientDTO(clientById);
            }
            catch (Exception)
            {
                throw new Exception("No se pudieron obtener todos los clientes a través de su Id");
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            try
            {
                return _clientRepository.GetAllClients();

                /*IEnumerable<Client> clients = _clientRepository.GetAllClients();
                return clients.Select(client => new Client(client)).ToList(); */

            }
            catch (Exception)
            {

                throw new Exception("No se pudieron obtener todos los clientes");
            }
        }
    }
}
