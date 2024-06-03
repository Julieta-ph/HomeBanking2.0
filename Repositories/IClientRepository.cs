using HomeBanking2._0.Models;

namespace HomeBanking2._0.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);

    }
}
