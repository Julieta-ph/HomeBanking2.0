﻿namespace HomeBanking2._0.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public Account Account { get; set; }

        public long AccountId { get; set; }

    }
}
