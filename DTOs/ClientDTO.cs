using HomeBanking2._0.Models;
using System.Text.Json.Serialization;

namespace HomeBanking2._0.DTOs
{
    public class ClientDTO
    {
        [JsonIgnore]

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<AccountDTO> Accounts { get; set; }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            LastName = client.LastName;
            FirstName = client.FirstName;
            Email = client.Email; 
            Password = client.Password;
            Accounts = client.Accounts.Select(a => new AccountDTO(a)).ToList();
                

        }

    }
}
