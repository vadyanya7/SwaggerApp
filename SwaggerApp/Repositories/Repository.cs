using Microsoft.EntityFrameworkCore;
using SwaggerApp.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        protected DbSet<T> Entities { get; set; }
        public Repository(ApplicationContext context)
        {
            _context = context;
            Entities = _context.Set<T>(); 
        } 
        public void Add(T entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public void Update(int id, T entity)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
        public T GetWithInclude(int id,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.FirstOrDefault(x=>x.Id==id);
        }
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Entities.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
