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
             string query = "INSERT INTO Users (Name, SurName,Age, OfficeId) VALUES(@Name, @SurNAme, @Age,@OfficeId)";
            _users.Add(query,user);
        }

        public void DeleteUser(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";
            _users.Delete(query, id);
        }

        public User GetUser(int id)
        {
            string query = "SELECT * FROM Users";
            return _users.GetWithInclude(query, id, p => p.Office, i => i.Tasks);
        }

        public List<User> GetUsers()
        {
            var list = _users.GetAll("SELECT * FROM Users")
                      .Include(x => x.Office).Include(x=>x.Tasks).ToList();
            return list;
        }

        public void UpdateUser(int id, User user)
        {
            string query = "UPDATE Users SET" +
                    " Name = @Name, SurName = @SurName, Age = @Age, OfficeId = @OfficeID" +
                    " WHERE Id = @Id"; 
            _users.Update(query, id, user);
        }
    }
}
