using System.Collections.Generic;
using System.Linq;
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
            _tasks.Add(task);
        }

        public void DeleteTask(int id)
        {
            _tasks.Delete(id);
        }

        public Task GetTask(int id)
        {
            return _tasks.GetWithInclude(id, p => p.User);
        }

        public List<Task> GetTasks()
        {
            return _tasks.GetAll().ToList();
        }

        public void UpdateTask(int id, Task task)
        {
            _tasks.Update(id, task);
        }
    }
}
