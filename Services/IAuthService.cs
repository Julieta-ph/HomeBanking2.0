using HomeBanking2._0.DTOs;
using System.Security.Claims;

namespace HomeBanking2._0.Services
{
    public interface IAuthService
    {
        ClaimsIdentity GenerateClaim(ClientLoginDTO clientLoginDTO);

    }
}
