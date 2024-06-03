using HomeBanking2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Repositories.Implementations
{
    
        public class ClientRepository : RepositoryBase<Client>, IClientRepository
        {
            public ClientRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
            {
            }

            public Client FindById(long id)
            {
                return FindByCondition(client => client.Id == id)
                    .Include(client => client.Accounts)
                    .FirstOrDefault();
            }

            public IEnumerable<Client> GetAllClients()
            {
                return FindAll()
                    .Include(client => client.Accounts)
                    .ToList();
            }

            public void Save(Client client)
            {
                Create(client);
                SaveChanges();
            }
        }
    
}
