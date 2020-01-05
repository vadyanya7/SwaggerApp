using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private DbService _dbService;

        public TaskController(ApplicationContext context)
        {
            _dbService = new DbService(context);
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            var tasks = _dbService.GetTasks();
            return tasks;
        }


        [HttpGet("{id}")]
        public Task Get(int id)
        {
            var task = _dbService.GetTask(id);
            return task;
        }

        [HttpPost]
        [Produces("application/json")]
        public Task Post([FromBody] Task task)
        {
            _dbService.AddTask(task);
            return task;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task task)
        {
            _dbService.UpdateTask(id, task);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dbService.DeleteTask(id);
        }

    }
}