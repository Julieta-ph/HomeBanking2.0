﻿using HomeBanking2._0.Models;

namespace HomeBanking2._0.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();

        void Save(Account Account);

        Account FindById(long id);

       
    }
}
