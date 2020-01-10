using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Models;
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
            Func<Office, User, Task, Office> lambda = (offic, user, task) =>
            {
              offic.User = user;
              offic.User.Tasks = new List<Task> {task};// с этим была проблема
              return offic;// хотел лямбду в иетод пробросить ((

            };
           return _offices.Get( id );
        }

        public List<Office> GetOffices()
        {          
            return _offices.GetAll()
                   .ToList(); 
        }

        public void UpdateOffice(int id, Office office)
        {
           
            _offices.Update(id, office);
        }
    }
}
