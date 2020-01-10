using System.Collections.Generic;
using System.Linq;
using Swagger.Models;
using SwaggerApp.Repositories;
namespace SwaggerApp.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IRepoOffice _offices;

        public OfficeService(IRepoOffice officeRepository)
        {
            _offices = officeRepository;
        }
        public void AddOffice(Office office)
        {
            _offices.Add(office);
        }

        public void DeleteOffice(int id)
        {
            _offices.Delete(id);
        }

        public Office GetOffice(int id)
        {
            return _offices.Get(id);
        }

        public List<Office> GetOffices()
        {          
            return _offices.GetAll().ToList(); 
        }

        public void UpdateOffice(int id, Office office)
        {
            _offices.Update(id, office);
        }
    }
}
