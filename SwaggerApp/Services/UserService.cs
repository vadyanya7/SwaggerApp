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
            return _users.GetWithInclude(id, p => p.Office, i => i.Tasks);
        }

        public List<User> GetUsers()
        {
            var list = _users.GetAll()
                      .Include(x => x.Office).Include(x=>x.Tasks).ToList();
            return list;
        }

        public void UpdateUser(int id, User user)
        {
            _users.Update(id, user);
        }
    }
}
