using System.Collections.Generic;
using System.Linq;
using Swagger.Models;
using SwaggerApp.Repositories;

namespace SwaggerApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepoUser _users;

        public UserService(IRepoUser userRepository)
        {
            _users = userRepository;
        }
        public void AddUser(User user)
        {
            _users.Add(user);
            _users.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _users.Delete(id);
            _users.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _users.Get(id);
        }

        public List<User> GetUsers()
        {
            var list = _users.GetAll().ToList();
            return list;
        }

        public void UpdateUser(int id, User user)
        {
            _users.Update(id, user);
            _users.SaveChanges();
        }
    }
}
