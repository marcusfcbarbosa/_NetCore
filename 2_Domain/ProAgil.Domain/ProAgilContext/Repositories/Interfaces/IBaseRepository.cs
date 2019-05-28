using System;
using System.Threading.Tasks;

namespace ProAgil.Domain.ProAgilContext.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(long id);
        T GetById(Guid id);
        Task<bool> SaveChangesAsync();
    }
}