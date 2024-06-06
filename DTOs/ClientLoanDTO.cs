using HomeBanking2._0.Models;

namespace HomeBanking2._0.DTOs
{
    public class ClientLoanDTO
    {
        public long Id { get; set; }
        public long LoanId { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }
        public int Payments { get; set; }

        public ClientLoanDTO() { }

        public ClientLoanDTO(ClientLoan clientloan)
        {
            Id = clientloan.Id;
            LoanId = clientloan.LoanId;
            Name = clientloan.Loan.Name;
            Amount = clientloan.Amount;
            Payments = Convert.ToInt32(clientloan.Payments);

        }
    }
}
