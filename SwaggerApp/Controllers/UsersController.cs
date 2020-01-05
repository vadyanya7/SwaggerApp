using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbService _dbService;

        public UsersController(ApplicationContext context)
        {
            _dbService = new DbService(context);
        }

        [HttpGet]
        [Route("api/users/get")]
        public IEnumerable<User> Get()
        {
            var users = _dbService.GetUsers();
            return users;
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _dbService.GetUser(id);
            return user;
        }

        [HttpPost]
        [Produces("application/json")]
        public User Post([FromBody] User user)
        {
            _dbService.AddUser(user);
            return user;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            _dbService.UpdateUser(id, user);
        
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dbService.DeleteUser(id);
        }
    }
}
