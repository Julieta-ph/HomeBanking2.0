using HomeBanking2._0.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeBanking2._0.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public TransactionDTO(Transaction transaction)
        {
            Id = transaction.Id;
            Type = transaction.Type;
            Amount = transaction.Amount;
            Description = transaction.Description;
            CreationDate = transaction.CreationDate;
    

        }


    }
}
