using HomeBanking2._0.Models;

namespace HomeBanking2._0.DTOs
{
    public class ClientLoginDTO
    {
        public long Id { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }

        public ClientLoginDTO(ClientLogin login)
        {
            Id = login.Id;
            Email = login.Email;
            Password = login.Password;
        }
    }
}
