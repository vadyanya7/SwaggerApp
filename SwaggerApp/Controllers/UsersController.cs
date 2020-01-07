using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;
using SwaggerApp.Services;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        [Route("api/users/get")]
        public IEnumerable<User> Get()
        {
            var users = _userService.GetUsers();
            return users;
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _userService.GetUser(id);
            return user;
        }

        [HttpPost]
        [Produces("application/json")]
        public User Post([FromBody] User user)
        {
            _userService.AddUser(user);
            return user;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            _userService.UpdateUser(id, user);       
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}
