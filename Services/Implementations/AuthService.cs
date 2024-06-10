using HomeBanking2._0.DTOs;
using HomeBanking2._0.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace HomeBanking2._0.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public ClaimsIdentity GenerateClaim(ClientLoginDTO clientLoginDTO)
        {
            throw new NotImplementedException();
        }

        /* public ClaimsIdentity GenerateClaim(ClientLoginDTO clientLoginDTO)
         {
             try
             {
                 Client user = _clientService.GetClientByEmail(ClientLoginDTO.Email);

                 if (user == null || !String.Equals(user.Password, ClientLoginDTO.Password))

                 {
                     throw new Exception("El usuario no fue encontrado");
                 }
                 var claims = new List<Claim>
                 {
                     new Claim("Client", user.Email)
                 };

                 if (user.Email == "jph453@gmail.com")
                 {
                     claims.Add(new Claim("Admin", "true"));
                 }

                 var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

             }
             catch (Exception ex)
             {
                 throw new Exception("Error");
             }
         } */
    }
}
