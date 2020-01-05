using Swagger.Models;
using SwaggerApp.Repositories;
using System.Collections.Generic;
using Task = Swagger.Models.Task;

namespace SwaggerApp
{
    public class DbService
    {
        private readonly IRepository<User> _users;
        private readonly IRepository<Office> _offices;
        private readonly IRepository<Task> _tasks;

        public DbService(ApplicationContext context)
        {
            _users = new Repository<User>(context);
            _offices = new Repository<Office>(context);
            _tasks = new Repository<Task>(context);
        }

        public List<User> GetUsers()
        {
            return _users.GetAll();
        }

        public List<Office> GetOffices()
        {
            return _offices.GetAll();
        }

        public List<Task> GetTasks()
        {
            return _tasks.GetAll();
        }

        public User GetUser(int id)
        {
            return _users.Get(id);
        }
        public Office GetOffice(int id)
        {
            return _offices.Get(id);
        }

        public Task GetTask(int id)
        {
            return _tasks.Get(id);
        }
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void AddOffice(Office office)
        {
            _offices.Add(office);
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public void UpdateUser(int id, User user)
        {
            _users.Update(id, user);
        }

        public void UpdateOffice(int id, Office office)
        {
            _offices.Update(id, office);
        }

        public void UpdateTask(int id, Task task)
        {
            _tasks.Update(id, task);
        }

        public void DeleteUser(int id)
        {
            _users.Delete(id);
        }


        public void DeleteOffice(int id)
        {
            _offices.Delete(id);
        }

        public void DeleteTask(int id)
        {
            _tasks.Delete(id);
        }
    }
}
