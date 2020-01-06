using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Repositories;

namespace SwaggerApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _users;

        public UserService(IRepository<User> userRepository)
        {
            _users = userRepository;
        }
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void DeleteUser(int id)
        {
            _users.Delete(id);
        }

        public User GetUser(int id)
        {
            var ss = _users.Get(id);
            return ss;
        }

        public List<User> GetUsers()
        {
            var list = _users.GetAll().Include(c => c.Office)
                      .Include(x => x.Tasks).ToList();

            return list;
        }

        public void UpdateUser(int id, User user)
        {
            _users.Update(id, user);
        }
    }
}
