using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaggerApp.Services;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;

        public TaskController(TaskService service)
        {
            _taskService = service;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            var tasks = _taskService.GetTasks();
            return tasks;
        }


        [HttpGet("{id}")]
        public Task Get(int id)
        {
            var task = _taskService.GetTask(id);
            return task;
        }

        [HttpPost]
        [Produces("application/json")]
        public Task Post([FromBody] Task task)
        {
            _taskService.AddTask(task);
            return task;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task task)
        {
            _taskService.UpdateTask(id, task);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _taskService.DeleteTask(id);
        }

    }
}