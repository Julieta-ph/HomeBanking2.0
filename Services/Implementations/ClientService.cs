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

        public Client GetClientById(long id)
        {
            try
            {
                return _clientRepository.FindById(id);
            }
            catch (Exception)
            {
                throw new Exception("No se pudieron obtener todos los clientes a través de su Id");
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

        public IEnumerable<Client> GetAllClients()
        {
            try
            {
                return _clientRepository.GetAllClients();
            }
            catch (Exception)
            {

                throw new Exception("No se pudieron obtener todos los clientes");
            }
        }

     

    }
}
