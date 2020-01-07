using System.Collections.Generic;
using Swagger.Models;

namespace SwaggerApp.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
