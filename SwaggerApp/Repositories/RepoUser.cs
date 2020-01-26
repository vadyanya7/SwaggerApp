using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace SwaggerApp.Repositories
{
    public class RepoUser : IRepoUser, IUserStore<User>, IUserPasswordStore<User>
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
        }

        public void Delete(int id)
        {
            var item = Entities.FirstOrDefault(x => x.Id == id);
            Entities.Remove(item);
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
            } 
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return Task.Run(() => { user.UserName = userName; });
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(() => { user.NormalizedUserName = normalizedName; });
        }
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            Entities.Add(user);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var item = Entities.FirstOrDefault(x => x.Id == user.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();               
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            Entities.Remove(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task<User>.Run(() =>
            {
              return Get(Convert.ToInt32(userId));
            });
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task<User>.Run(() =>
            {
                return Entities.Include(u => u.Office).Include(u => u.Tasks).FirstOrDefault(x => x.UserName == normalizedUserName);
            });
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Run(() => { user.PasswordHash = passwordHash; });
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash == null ? false : true);
        }
    }
}
