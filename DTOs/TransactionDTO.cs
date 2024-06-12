using HomeBanking2._0.Models;

namespace HomeBanking2._0.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public long ClientId { get; set; }

    }
}
