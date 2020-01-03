using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/users/get")]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _context.Users.ToList().Find(e => e.Id == id);
        }

        [HttpPost]
        [Produces("application/json")]
        public User Post([FromBody] User user)
        {
            User newUser = new User();
            newUser.Name = user.Name;
            newUser.Age = user.Age;
            newUser.SurName = user.SurName;
            newUser.Office = user.Office;
            _context.Users.Add(user);
            _context.SaveChanges();
            return newUser;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            var changedUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (changedUser != null)
            {
                changedUser.Name = user.Name;
                changedUser.Age = user.Age;
                changedUser.SurName = user.SurName;
                changedUser.Office = user.Office;
                _context.SaveChanges();
            }

        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
