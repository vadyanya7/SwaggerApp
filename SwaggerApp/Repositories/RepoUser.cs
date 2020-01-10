using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using System.Linq;

namespace SwaggerApp.Repositories
{
    public class RepoUser : IRepoUser
    {
        private ApplicationContext _context;
        protected DbSet<User> Entities { get; set; }

        public RepoUser(ApplicationContext context)
        {
            _context = context;
            Entities = _context.Set<User>();
        }
        public void Add(User entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            Entities.Remove(item);
            _context.SaveChanges();
        }

        public User Get(int id)
        {
            return Entities.Include(u => u.Office).Include(u => u.Tasks).FirstOrDefault(x=>x.Id==id);
        }

        public IQueryable<User> GetAll()
        {
           return Entities.Include(u=>u.Office).Include(u=>u.Tasks);
        }

        public void Update(int id, User entity)
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
