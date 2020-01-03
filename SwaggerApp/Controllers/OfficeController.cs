using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private ApplicationContext _context;

        public OfficeController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IEnumerable<Office> Get()
        {
            return _context.Office.ToList();
        }


        [HttpGet("{id}")]
        public Office Get(int id)
        {
            return _context.Office.ToList().Find(e => e.Id == id);
        }

        [HttpPost]
        [Produces("application/json")]
        public Office Post([FromBody] Office office)
        {
            _context.Office.Add(office);
            _context.SaveChanges();
            return office;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Office office)
        {
            var changedOffice = _context.Office.FirstOrDefault(x => x.Id == id);
            if (changedOffice != null)
            {
                changedOffice.Id = office.Id;
                changedOffice.Name = office.Name;
                changedOffice.User = office.User;
                changedOffice.UserId = office.UserId;
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var office = _context.Office.FirstOrDefault(x => x.Id == id);
            _context.Office.Remove(office);
            _context.SaveChanges();
        }

    }
}