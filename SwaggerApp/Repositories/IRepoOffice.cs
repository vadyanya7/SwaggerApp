using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public interface IRepoOffice
    {
        IQueryable<Office> GetAll();
        Office Get(int id);
        void Add(Office entity);
        void Update(int id, Office entity);
        void Delete(int id);
      
    }
}
