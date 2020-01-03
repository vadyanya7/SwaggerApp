using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ApplicationContext _context;

        public TaskController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _context.Tasks.ToList();
        }


        [HttpGet("{id}")]
        public Task Get(int id)
        {
            return _context.Tasks.ToList().Find(e => e.Id == id);
        }

        [HttpPost]
        [Produces("application/json")]
        public Task Post([FromBody] Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task task)
        {
            var changedTask = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (changedTask != null)
            {
                changedTask.Id = task.Id;
                changedTask.User = task.User;
                changedTask.TaskDescription = task.TaskDescription;
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

    }
}