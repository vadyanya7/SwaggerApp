using Dapper;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SwaggerApp.Repositories
{
    public class RepoTask :IRepoTask
    {
        private string _connectionString;
        public RepoTask(string connection)
        {
            _connectionString = connection;
        }
        public void Add(Task entity)
        {
            string query = "INSERT INTO Tasks (TaskDescription, UserId) VALUES(@TaskDescription, @UserId)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, new { id });
            }
        }

        public Task Get(int id)
        {
            string query = "select * FROM [AllDB].[dbo].[Tasks] full outer join Users on Users.Id= Tasks.Id  inner join Office on Office.Id=Users.OfficeId";

            using (var db = new SqlConnection(_connectionString))
            {
                var tasks = new List<Task>();

                var offices = db.Query<Task, User, Office, Task>(
                query, (task, user, office) =>
                {
                    task.User = user;

                    task.User.Office = office;

                    return task;
                },
                commandType: CommandType.Text).FirstOrDefault(x => x.Id == id);
                offices.User.Tasks.AddRange(tasks);
                return offices;
            }
        }
        public IQueryable<Task> GetAll()
        {
            string query = "select * FROM [AllDB].[dbo].[Tasks] full outer join Users on Users.Id= Tasks.Id  inner join Office on Office.Id=Users.OfficeId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var tasks = new List<Task>();
                var list = GetOffices((SqlConnection)db, query, tasks);

                return list.AsQueryable();
            };
        }

        public void Update(int id, Task entity)
        {
            string query = "UPDATE Tasks SET" +
                     " TaskDescription = @TaskDescription, UserId=@UserId WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }

        private List<Task> GetOffices(SqlConnection db, string query, List<Task> tasks)
        {
            List<Task> list = db.Query<Task, User, Office, Task>(
               query, (task, user, office) =>
               {
                   task.User = user;

                   task.User.Office = office;

                   return task;
               },
               commandType: CommandType.Text).ToList();
            return list;
        }

    }
}
