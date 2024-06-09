﻿using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{
    public interface IClientService
    {
        long SaveAndReturnIdClient(Client client);
        Client GetClientByEmail(string email);

        Client GetClientById(long id);

        IEnumerable<Client> GetAllClients();

    }
}
