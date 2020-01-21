using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Swagger.Models;
using SwaggerApp.Models;

namespace SwaggerApp.Services
{
    public interface IUserService
    {
        List<User> GetUsers();  
        User GetUser(string userName);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(string userName, User user);
        void DeleteUser(string userName);
        ResponseModel Token(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(User model);
    }
}
