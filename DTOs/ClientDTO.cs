using HomeBanking2._0.Models;
using System.Text.Json;
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
        // no se muestra porque es un dato sensible public string Password { get; set; }

        public ICollection<AccountClientDTO> Accounts { get; set; }

        public ICollection<ClientLoanDTO> Loans { get; set; }

        public ICollection<CardDTO> Cards { get; set; }

        
       
        public ClientDTO(){}

        public ClientDTO(Client client)
        {
            Id = client.Id;
            LastName = client.LastName;
            FirstName = client.FirstName;
            Email = client.Email;    
            Accounts = client.Accounts.Select(a => new AccountClientDTO(a)).ToList();
            Loans = client.ClientLoans.Select(l => new ClientLoanDTO(l)).ToList();
            Cards = client.Cards.Select(c => new CardDTO(c)).ToList();
            
        }

       
    }
}
