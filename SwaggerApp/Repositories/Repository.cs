using Dapper;
using Microsoft.EntityFrameworkCore;
using SwaggerApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace SwaggerApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private string _connectionString;
        public Repository(string connection)
        {
            _connectionString = connection;
        }
        public void Add(string query, T entity)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }
        public void Delete(string query, int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, new { id });
            }
        }

        public T Get(string query, int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(query, new { id }).FirstOrDefault();
            }
           
        }

        public IQueryable<T> GetAll(string query)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(query).AsQueryable();
            };
        }

        public void Update(string query, int id, T entity)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(query, entity);
            }
        }
        public T GetWithInclude(string query, int id,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var querySql = Include(query,includeProperties);
            return querySql.FirstOrDefault(x=>x.Id==id);
        }
        private IQueryable<T> Include(string queryToAll, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetAll(queryToAll).AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
