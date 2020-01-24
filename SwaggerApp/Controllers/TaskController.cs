using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerApp.Models.ViewModels;
using SwaggerApp.Services;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;
        private readonly IMapper _mapper;
        public TaskController(ITaskService service, IMapper mapper)
        {
            _taskService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            var listModel = new List<TaskModel>();
            var tasks = _taskService.GetTasks();
            foreach(var t in tasks)
            {
                listModel.Add(_mapper.Map<Task,TaskModel>(t));
            }
            return listModel;
        }


        [HttpGet("{id}")]
        public TaskModel Get(int id)
        {
            var task = _taskService.GetTask(id);
            var taskModel = _mapper.Map<Task, TaskModel>(task);
            return taskModel;
        }

        [HttpPost]
        [Produces("application/json")]
        public TaskModel Post([FromBody] TaskModel taskModel)
        {
            var task = _mapper.Map<TaskModel, Task>(taskModel);
            _taskService.AddTask(task);
            return taskModel;
        }

        [HttpPut("{id}")]
        public UpdateTaskModel Put(int id, [FromBody] UpdateTaskModel taskModel)
        {
            var task = _mapper.Map< UpdateTaskModel, Task>(taskModel);
            _taskService.UpdateTask(id, task);
            return taskModel;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _taskService.DeleteTask(id);
        }

    }
}