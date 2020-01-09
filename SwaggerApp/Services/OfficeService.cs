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
            string query = "INSERT INTO Office (Name) VALUES(@Name)";
            _offices.Add(query,office);
        }

        public void DeleteOffice(int id)
        {
            string query = "DELETE FROM Office WHERE Id = @id";
            _offices.Delete(query,id);
        }

        public Office GetOffice(int id)
        {
            string query = "Select * From Office";
            return _offices.GetWithInclude(query, id, p => p.User, o => o.User.Tasks);
        }

        public List<Office> GetOffices()
        {          
            return _offices.GetAll("SELECT * FROM Office").Include(x => x.User)
                   .ThenInclude(c => c.Tasks).ToList(); 
        }

        public void UpdateOffice(int id, Office office)
        {
            string query = "UPDATE Office SET" +
                    " Name = @Name WHERE Id = @Id";
            _offices.Update(query,id, office);
        }
    }
}
