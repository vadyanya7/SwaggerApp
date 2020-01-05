using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private DbService _dbService;

        public OfficeController(ApplicationContext context)
        {
            _dbService = new DbService(context);
        }

        [HttpGet]
        public IEnumerable<Office> Get()
        {
            var offices = _dbService.GetOffices();
            return offices;
        }


        [HttpGet("{id}")]
        public Office Get(int id)
        {
            var office = _dbService.GetOffice(id);
            return office;
        }

        [HttpPost]
        [Produces("application/json")]
        public Office Post([FromBody] Office office)
        {
            _dbService.AddOffice(office);
            return office;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Office office)
        {
            _dbService.UpdateOffice(id,office);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dbService.DeleteOffice(id);       
        }

    }
}