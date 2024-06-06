using HomeBanking2._0.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using HomeBanking2._0.Models;
using HomeBanking2._0.DTOs;


namespace HomeBanking2._0.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;


        public AuthController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ClientLoginDTO clientlogin)
        {
            //creamos la identidad

            try
            {
                Client user = _clientRepository.FindByEmail(clientlogin.Email);
                if (user == null)

                    return StatusCode(403, "Usuario no encontrado");
                if (!user.Password.Equals(clientlogin.Password))
                    return StatusCode(403, "Credenciales Inválidas");

                //creamos los claims si lo anterior está ok, tomamos la info de using System.Security.Claims;

                var claims = new List<Claim>
                {
                    new Claim("Client", user.Email)
                };                

                //creamos la identidad de esta claim

                var claimsIdentity = new ClaimsIdentity(
                    claims,

                    //le pedimos que utilice esto que viene del program

                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                //retornamos la cookie al navegador

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity) //las claims son info extra que podes agregar dentro de una cookie
                );
                return Ok(clientlogin);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
