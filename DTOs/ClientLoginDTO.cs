using HomeBanking2._0.Models;

namespace HomeBanking2._0.DTOs
{
    public class ClientLoginDTO
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ClientLoginDTO(ClientLogin clientLogin)
        {
            Id = clientLogin.Id;
            LastName = clientLogin.LastName;
            FirstName = clientLogin.FirstName;
            Email = clientLogin.Email;
            Password = clientLogin.Password;
        }
    }
}
