using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SwaggerApp.Repositories;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> _tasks;

        public TaskService(IRepository<Task> taskRepository)
        {
            _tasks = taskRepository;
        }
        public void AddTask(Task task)
        {
            string query = "INSERT INTO Tasks (TaskDescription, UserId) VALUES(@TaskDescription, @UserId)";
            _tasks.Add(query, task);
        }

        public void DeleteTask(int id)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";
            _tasks.Delete(query,id);
        }

        public Task GetTask(int id)
        {
            string query = "Select * From Tasks";
            return _tasks.GetWithInclude(query,id, p => p.User, o=>o.User.Office);
        }

        public List<Task> GetTasks()
        {
            return _tasks.GetAll("Select * From Tasks").Include(x=>x.User).ThenInclude(c=>c.Office).ToList();
        }

        public void UpdateTask(int id, Task task)
        {
            string query = "UPDATE Tasks SET" +
                    "TaskDescription = @TaskDescription, UserId=@UserId WHERE Id = @Id";
            _tasks.Update(query, id, task);
        }
    }
}
