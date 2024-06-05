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
      

        public ICollection<AccountClientDTO> Accounts { get; set; }

        public ICollection<ClientLoanDTO> Loans { get; set; }

        public ICollection<CardDTO> Cards { get; set; }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            LastName = client.LastName;
            FirstName = client.FirstName;
            Email = client.Email; 
            Accounts = client.Accounts.Select(a => new AccountClientDTO(a)).ToList();
            Loans = client.ClientLoans.Select(a => new ClientLoanDTO(a)).ToList();
            Cards = client.Cards.Select(a => new CardDTO(a)).ToList();

        }

    }
}
