using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Repositories;
namespace SwaggerApp.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IRepository<Office> _offices;

        public OfficeService(IRepository<Office> officeRepository)
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
            return _offices.GetWithInclude(id, p => p.User, o => o.User.Tasks);
        }

        public List<Office> GetOffices()
        {          
            return _offices.GetAll().Include(x => x.User)
                   .ThenInclude(c => c.Tasks).ToList(); 
        }

        public void UpdateOffice(int id, Office office)
        {
            _offices.Update(id, office);
        }
    }
}
