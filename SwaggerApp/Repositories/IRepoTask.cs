using Swagger.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public interface IRepoTask
    {
        IQueryable<Task> GetAll();
        Task Get(int id);
        void Add(Task entity);
        void Update(int id, Task entity);
        void Delete(int id);
        void SaveChanges();
    }
}
