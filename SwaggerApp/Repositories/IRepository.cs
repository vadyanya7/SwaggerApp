using Microsoft.EntityFrameworkCore;
using SwaggerApp.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(string query);
        T Get(string query, int id);
        void Add(string query,T entity);
        void Update(string query, int id, T entity);
        void Delete(string query,int id);
        T GetWithInclude(string query, int id, params Expression<Func<T, object>>[] includeProperties);
    }
}
