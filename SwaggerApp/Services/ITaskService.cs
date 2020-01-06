using System.Collections.Generic;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Services
{
    interface ITaskService
    {
        List<Task> GetTasks();
        Task GetTask(int id);
        void AddTask(Task task);
        void UpdateTask(int id, Task task);
        void DeleteTask(int id);
    }
}
