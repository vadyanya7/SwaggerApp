using Swagger.Models;
using System.Collections.Generic;

namespace SwaggerApp.Services
{
    interface IOfficeService
    {
        List<Office> GetOffices();
        Office GetOffice(int id);
        void AddOffice(Office office);
        void UpdateOffice(int id, Office office);
        void DeleteOffice(int id);
      
    }
}
