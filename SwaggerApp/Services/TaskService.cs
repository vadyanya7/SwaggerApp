using System.Collections.Generic;
using System.Linq;
using SwaggerApp.Repositories;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepoTask _tasks;

        public TaskService(IRepoTask taskRepository)
        {
            _tasks = taskRepository;
        }
        public void AddTask(Task task)
        {
            _tasks.Add(task);
            _tasks.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            _tasks.Delete(id);
            _tasks.SaveChanges();
        }

        public Task GetTask(int id)
        {
            return _tasks.Get(id);
        }

        public List<Task> GetTasks()
        {
            return _tasks.GetAll().ToList();
        }

        public void UpdateTask(int id, Task task)
        {
            _tasks.Update(id, task);
            _tasks.SaveChanges();
        }
    }
}
