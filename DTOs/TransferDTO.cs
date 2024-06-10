using HomeBanking2._0.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeBanking2._0.DTOs
{
    public class TransferDTO
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

       

   

    }
}
