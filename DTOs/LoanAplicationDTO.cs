namespace HomeBanking2._0.DTOs
{
    public class LoanAplicationDTO
    {
        public long LoanId {  get; set; }
        public double Amount { get; set; }
        public string Payments {  get; set; }

        public string ToAccountNumber { get; set; }

    }
}
