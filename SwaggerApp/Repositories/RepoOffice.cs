using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using System.Linq;

namespace SwaggerApp.Repositories
{
    public class RepoOffice : IRepoOffice
    {
        private readonly ApplicationContext _context;
        protected DbSet<Office> Entities { get; set; }
        public RepoOffice(ApplicationContext context)
        {
            _context = context;
            Entities = _context.Set<Office>(); 
        } 
        public void Add(Office entity)
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

        public Office Get(int id)
        {
            var item = Entities.Include(x => x.User)
                   .ThenInclude(c => c.Tasks).FirstOrDefault(x => x.Id == id);
            return item;
        }

        public IQueryable<Office> GetAll()
        {
            return Entities.Include(x => x.User)
                   .ThenInclude(c => c.Tasks);
        }

        public void Update(int id, Office entity)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
