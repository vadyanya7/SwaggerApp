using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
 

namespace SwaggerApp.Repositories
{
    public class RepoTask : IRepoTask
    {
        private readonly ApplicationContext _context;
        protected DbSet<Task> Entities { get; set; }

        public RepoTask(ApplicationContext context)
        {
            _context = context;
            Entities = _context.Set<Task>();
        }
        public void Add(Swagger.Models.Task entity)
        {
            Entities.Add(entity);
        }

        public void Delete(int id)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                Entities.Remove(item);
            }
        }

        public Task Get(int id)
        {
            var item = Entities.Include(t => t.User).ThenInclude(u=>u.Office).
                FirstOrDefault(x => x.Id == id);

            return item;
        }

        public IQueryable<Task> GetAll()
        {
            return Entities.Include(t => t.User).ThenInclude(u => u.Office);
        }

        public void Update(int id, Task entity)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(entity);
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
