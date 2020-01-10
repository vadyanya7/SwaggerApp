using Dapper;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Task = Swagger.Models.Task;

namespace SwaggerApp.Repositories
{
    public class RepoUser : IRepoUser
    {
        private string _connectionString;
        public RepoUser(string connection)
        {
            _connectionString = connection;
        }
        public void Add(User entity)
        {
            string query = "INSERT INTO Users (Name, SurName,Age, OfficeId) VALUES(@Name, @SurNAme, @Age,@OfficeId)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, new { id });
            }
        }

        public User Get(int id)
        {
            string query = "select * FROM [AllDB].[dbo].[Users] inner join Office on Office.Id= Users.OfficeId  full outer join Tasks on Users.Id=Tasks.UserId";

            using (var db = new SqlConnection(_connectionString))
            {
                var tasks = new List<Task>();

                var users = db.Query<User, Office, Task, User>(
                query, (user, office, task) =>
                {
                    user.Office = office;

                    if (user.Tasks == null)
                    {
                        user.Tasks = new List<Task>();
                    }

                    if (task != null)
                    {
                        if (user.Id == id)
                        {
                            tasks.Add(task);
                        }
                    }

                    return user;
                },
                commandType: CommandType.Text).FirstOrDefault(x => x.Id == id);
                users.Tasks.AddRange(tasks);
                return users;
            }
        }
        public IQueryable<User> GetAll()
        {
            string query = "select * FROM [AllDB].[dbo].[Users] inner join Office on Office.Id= Users.OfficeId  full outer join Tasks on Users.Id=Tasks.UserId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var tasks = new List<Task>();
                var list = GetUsers((SqlConnection)db, query, tasks);

                return list.AsQueryable();
            };
        }

        public void Update(int id, User entity)
        {
            string query = "UPDATE Users SET" +
                   " Name = @Name, SurName = @SurName, Age = @Age, OfficeId = @OfficeID" +
                   " WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }

        private List<User> GetUsers(SqlConnection db, string query, List<Task> tasks)
        {
            var userDictionary = new Dictionary<int, User>();
            List<User> list = db.Query<User, Office, Task, User>(
               query, (user, office, task) =>
               {
                   User userEntry;

                   if (!userDictionary.TryGetValue(user.Id, out userEntry))
                   {
                       userEntry = user;
                       user.Office = office;
                       userEntry.Tasks = new List<Task>();
                       userDictionary.Add(userEntry.Id, userEntry);
                   }

                   userEntry.Tasks.Add(task);

                   return user;
               },
                commandType: CommandType.Text).Distinct().ToList();

            return list;
        }

    }
}
