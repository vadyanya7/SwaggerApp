using SwaggerApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwaggerApp.Services
{
    public interface IAuthenticateService
    {
         ResponseModel Token(ClaimsIdentity identity);
         ClaimsIdentity GetIdentity( Account model);
         List<Account> GetAccounts();
         Account GetAccount(string userName);
         Task<Account> AddAccount(Account account);
         void UpdateAccount(string userName, Account account);
         void DeleteAccount(string userName);
    }
}
