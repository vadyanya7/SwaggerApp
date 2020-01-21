using Swagger.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public interface IRepoUser
    {
        IQueryable<User> GetAll();
        User Get(string id);
        void Add(User entity);
        void Update(string id, User entity);
        void Delete(string id);
        void SaveChanges();
    }
}
