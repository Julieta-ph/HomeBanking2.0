﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HomeBanking2._0.Models
{
    public class HomeBankingContext : DbContext

    {
        public HomeBankingContext(DbContextOptions<HomeBankingContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<ClientLoan> ClientLoans { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<ClientLogin> ClientLogins { get; set; }


    }
}
