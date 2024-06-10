using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{
    public interface IClientService
    {
        /* long SaveAndReturnIdClient(Client client); */

        Client GetClientByEmail(string email);

        ClientDTO GetClientById(long id);

        IEnumerable<Client> GetAllClients();

        Client Save(NewClientDTO newClientDTO);

    }
}
