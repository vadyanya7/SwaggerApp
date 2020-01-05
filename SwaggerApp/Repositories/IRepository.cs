using System.Collections.Generic;

namespace SwaggerApp.Repositories
{
    public interface IRepository<T> 
    {
        List<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);

    }
}
