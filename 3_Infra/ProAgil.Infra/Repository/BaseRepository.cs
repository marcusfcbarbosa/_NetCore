
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;

namespace ProAgil.Infra.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ProAgilContext _context;
        protected DbSet<T> DbSet;
        
        public BaseRepository(ProAgilContext context){
            _context = context;
            DbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
        }
        public void Update(T entity)
        {
               _context.Entry<T>(entity).State = EntityState.Modified;
               Save();
        }
        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return ( await _context.SaveChangesAsync() > 0);
        }
        private void Save()
        {
           _context.SaveChanges();
        }
    }
}