namespace HomeBanking2._0.Models
{
    public class ClientLogin
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Client Client { get; set; }
        public long ClientId { get; set; }
    }
}
