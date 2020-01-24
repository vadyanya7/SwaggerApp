using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;
using SwaggerApp.Models.ViewModels;
using SwaggerApp.Services;

namespace SwaggerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private IOfficeService _officeService;
        private readonly IMapper _mapper;
        public OfficeController(IOfficeService service, IMapper mapper)
        {
            _officeService = service;
            _mapper = mapper;
        }
       
        [HttpGet]
        public IEnumerable<OfficeModel> Get()
        {
            var offices = _officeService.GetOffices();
            var models = new List<OfficeModel>();
            foreach(var office in offices)
            {
                 models.Add(_mapper.Map<Office, OfficeModel>(office));
            }            
            return models;
        }

       // 
        [HttpGet("{id}")]
        public OfficeModel Get(int id)
        {
            var office = _officeService.GetOffice(id);
            var model = _mapper.Map<Office, OfficeModel>(office);
            return model;
        }

        [HttpPost]
        [Produces("application/json")]
        public OfficeModel Post([FromBody] OfficeModel officeModel)
        {
            var office = _mapper.Map<OfficeModel, Office>(officeModel);
            _officeService.AddOffice(office);
            return officeModel;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OfficeModel officeModel)
        {
            var office = _mapper.Map<OfficeModel, Office>(officeModel);
            _officeService.UpdateOffice(id,office);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _officeService.DeleteOffice(id);       
        }

    }
}