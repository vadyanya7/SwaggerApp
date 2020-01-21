using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;
using SwaggerApp.Services;
using SwaggerApp.Models;
using SwaggerApp;
using Microsoft.AspNetCore.Identity;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }


        [HttpPost("/token")]
        public IActionResult Token([FromBody] User model)
        {
            var identity = _userService.GetIdentity(model);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var response = _userService.Token(identity);

            return Json(response);
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _userService.GetUsers();
            return users;
        }

        [HttpGet("{userName}")]
        public User Get(string userName)
        {
            var user = _userService.GetUser(userName);
            return user;
        }

        [HttpPost]
        public Task<User> Post([FromBody] User user)
        {
           var sd = _userService.AddUser(user);
            return sd;
        }

        [HttpPut("{userName}")]
        public Task<User> Put(string userName, [FromBody] User user)
        {
            var sr = _userService.UpdateUser(userName, user);
            return sr;
        }
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
            _userService.DeleteUser(userName);
        }
    }
}
