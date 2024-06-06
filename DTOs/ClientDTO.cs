using HomeBanking2._0.Models;
using System.Text.Json;


namespace HomeBanking2._0.DTOs
{
    public class ClientDTO 
    {
        

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<AccountDTO> Accounts { get; set; }

        public ICollection<ClientLoanDTO> Loans { get; set; }

        public ICollection<CardDTO> Cards { get; set; }

        public ICollection<ClientLoginDTO> Logins { get; set; }

        public ICollection<NewClientDTO> Clients { get; set; }

        public ClientDTO(){}

        public ClientDTO(Client clientDTO)
        {
            Id = clientDTO.Id;
            LastName = clientDTO.LastName;
            FirstName = clientDTO.FirstName;
            Email = clientDTO.Email; 
            Password = clientDTO.Password;
            Accounts = clientDTO.Accounts.Select(a => new AccountDTO(a)).ToList();
            Loans = clientDTO.ClientLoans.Select(l => new ClientLoanDTO(l)).ToList();
            Cards = clientDTO.Cards.Select(c => new CardDTO(c)).ToList();
            Logins = clientDTO.ClientLogins.Select(li => new ClientLoginDTO(li)).ToList();
            
        }

        public ClientDTO(NewClientDTO newClientDTO)
        {
            FirstName = newClientDTO.FirstName;
            LastName = newClientDTO.LastName;
            Email = newClientDTO.Email;

        }
    }
}
