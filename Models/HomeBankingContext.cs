using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Models
{
    public class HomeBankingContext : DbContext

    {
        public HomeBankingContext(DbContextOptions<HomeBankingContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

    }
}
