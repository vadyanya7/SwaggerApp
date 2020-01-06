using Microsoft.EntityFrameworkCore;
using SwaggerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        T GetWithInclude(int id,
            params Expression<Func<T, object>>[] includeProperties);
        T GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

    }
}
