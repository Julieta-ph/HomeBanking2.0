namespace HomeBanking2._0.Models
{
    public class DBInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                   new Client { FirstName="Victor", LastName="Coronado", Email = "vcoronado@gmail.com", Password="123456"},
                   new Client { FirstName="Julieta", LastName="Pinedo", Email = "jph453@gmail.com", Password="5248555"},
                   new Client { FirstName="María", LastName="Mendez", Email = "mendez453@gmail.com", Password="5248512"},
                   new Client { FirstName="Manuel", LastName="Antivero", Email = "manuelry@gmail.com", Password="365485"},
                   new Client { FirstName="Juan", LastName="Perez", Email = "luras342@gmail.com", Password="458127"},
                   new Client { FirstName="Lucía", LastName="Mariel", Email = "mariluci453@gmail.com", Password="954357"}

                };

                context.Clients.AddRange(clients);

                //guardamos
                context.SaveChanges();
            }

            if (!context.Account.Any())
            {
                var accountVictor = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (accountVictor != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 10000 },
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN002", Balance = 500 },
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN003", Balance = 15000 }
                        

                    };
                    foreach (Account account in accounts)
                    {
                        context.Account.Add(account);
                    }
                    context.SaveChanges();

                }
            }

        }
    }
}

