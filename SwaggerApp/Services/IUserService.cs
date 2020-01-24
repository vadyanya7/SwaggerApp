using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Swagger.Models;
using SwaggerApp.Models;

namespace SwaggerApp.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        Task<User> GetUser(string userName);
        Task<IdentityResult> AddUser(User user, string password);
        Task<IdentityResult> UpdateUser(string userName, User user);
        Task<IdentityResult> DeleteUser(string userName);
        ResponseModel Token(ClaimsIdentity identity);
        Task<ClaimsIdentity> GetIdentity(string userName, string password);
    }
}
