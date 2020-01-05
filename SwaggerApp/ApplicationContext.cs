using Microsoft.EntityFrameworkCore;
using Swagger.Models;

namespace SwaggerApp
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
              : base(options)
        {
           Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Swagger.Models.Task> Tasks { get; set; }
        public DbSet<Office> Office { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>()
                .HasOne(a => a.User)
                .WithOne(b => b.Office)
                .HasForeignKey<User>(b => b.OfficeId);
            modelBuilder.Entity<User>()
                .HasMany<Task>(c => c.Tasks)
                .WithOne(e => e.User)
                .HasForeignKey(b=>b.UserId);
        }
    }
}
