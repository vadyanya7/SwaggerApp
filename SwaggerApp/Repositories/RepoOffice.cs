using Dapper;
using Microsoft.EntityFrameworkCore;
using Swagger.Models;
using SwaggerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public class RepoOffice : IRepoOffice
    {
        private string _connectionString;
        public RepoOffice(string connection)
        {
            _connectionString = connection;
        }
        public void Add( Office entity)
        {
            string query = "INSERT INTO Office (Name) VALUES(@Name)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }
        public void Delete( int id)
        {
            string query = "DELETE FROM Office WHERE Id = @id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, new { id });
            }
        }

        public Office Get(int id)
        {
            string query = "select * FROM [AllDB].[dbo].[Office] inner join Users on Users.officeId= Office.Id  full outer join Tasks on Users.Id=Tasks.UserId";

            using (var db = new SqlConnection(_connectionString))
            {
               var tasks = new List<Task>();

               var offices = db.Query<Office, User, Task, Office>(
               query, (offic, user, task) =>
               {
                 offic.User = user;
                
                 if (offic.User.Tasks == null) 
                 {
                       offic.User.Tasks = new List<Task>();
                 }

                   if (task != null)
                   {
                       if(user.Id == id)
                       {
                           tasks.Add(task);
                       }
                   }
                   
                return offic;
               },
               commandType: CommandType.Text).FirstOrDefault(x=>x.Id==id);
               offices.User.Tasks.AddRange(tasks);
               return offices;
            }         
        }
        public IQueryable<Office> GetAll()
        {
            string query = "select * FROM [AllDB].[dbo].[Office] inner join Users on Users.officeId= Office.Id  full outer join Tasks on Users.Id=Tasks.UserId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var tasks = new List<Task>();
                var list = GetOffices((SqlConnection)db,query,tasks);
               
                return list.AsQueryable();
            };
        }

        public void Update(int id, Office entity)
        {
            string query = "UPDATE Office SET" +
                   " Name = @Name WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }

        private List<Office> GetOffices(SqlConnection db, string query,List<Task> tasks)
        {
            var officeDictionary = new Dictionary<int, Office>();
            List<Office> list =  db.Query<Office, User, Task, Office>(
               query, (offic, user, task) =>
               {
                   Office officeEntry;
                  
                   if (!officeDictionary.TryGetValue(offic.Id,out officeEntry ))
                   {
                       officeEntry = offic;
                       offic.User = user;
                       officeEntry.User.Tasks = new List<Task>();
                       officeDictionary.Add(officeEntry.Id, officeEntry);
                   }

                   officeEntry.User.Tasks.Add(task);               

                   return officeEntry;
               },
               commandType: CommandType.Text).Distinct().ToList();
            return list;
        }
      
    }
}
