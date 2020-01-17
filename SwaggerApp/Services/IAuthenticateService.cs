using SwaggerApp.Models;
using System.Security.Claims;

namespace SwaggerApp.Services
{
    public interface IAuthenticateService
    {
         ResponseModel Token(ClaimsIdentity identity);
         ClaimsIdentity GetIdentity(string username, string password);
    }
}
