using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swagger.Models;
using SwaggerApp.Repositories;
using SwaggerApp.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<OfficeService>();
            services.AddScoped<UserService>();
            services.AddScoped<TaskService>();
            services.AddSingleton<IRepository<User>>(p => new Repository<User>(connection));
            services.AddSingleton<IRepository<Office>>(p => new Repository<Office>(connection));
            services.AddSingleton<IRepository<Task>>(p => new Repository<Task>(connection));
            services.AddScoped(typeof(IOfficeService),typeof(OfficeService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(ITaskService), typeof(TaskService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
