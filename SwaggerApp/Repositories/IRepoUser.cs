using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SwaggerApp.Repositories
{
    public interface IRepoUser
    {
        IQueryable<User> GetAll();
        User Get(int id);
        void Add(User entity);
        void Update(int id, User entity);
        void Delete(int id);
    }
}
