using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Models;

namespace SwaggerApp
{
    public class ApplicationContext: IdentityDbContext<Account>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
              : base(options)
        {
           Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<User> Users1 { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Office> Office { get; set; }
        
        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>()
                .HasOne(a => a.User)
                .WithOne(b => b.Office)
                .HasForeignKey<User>(b => b.OfficeId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Tasks)
                .WithOne(x => x.User);

            modelBuilder.Entity<Account>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
