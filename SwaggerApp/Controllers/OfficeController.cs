using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;
using SwaggerApp.Services;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private IOfficeService _officeService;

        public OfficeController(IOfficeService service)
        {
            _officeService = service;
        }

        [HttpGet]
        [Authorize]
        [Route("getlogin")]
        public IEnumerable<Office> Get()
        {
            var offices = _officeService.GetOffices();
            return offices;
        }


        [HttpGet("{id}")]
        public Office Get(int id)
        {
            var office = _officeService.GetOffice(id);
            return office;
        }

        [HttpPost]
        [Produces("application/json")]
        public Office Post([FromBody] Office office)
        {
            _officeService.AddOffice(office);
            return office;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Office office)
        {
            _officeService.UpdateOffice(id,office);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _officeService.DeleteOffice(id);       
        }

    }
}