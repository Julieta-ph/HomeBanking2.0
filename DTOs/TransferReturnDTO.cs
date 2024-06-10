namespace HomeBanking2._0.DTOs
{
    public class TransferReturnDTO
    {
        public ICollection<TransactionDTO> Transactions { get; set; }

        public string MessageStatusCode { get; set; }

        public int StatusCode { get; set; }

        

    }
}
