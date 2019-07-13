using System;
using System.Collections.Generic;
using System.Linq;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;
using ProAgil.Shared.Interfaces;

namespace ProAgil.Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : class, IEntity
    {
        private readonly ProAgilContext _context;

        public BaseRepository(ProAgilContext context)
        {
            _context = context;
        }


        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(int id)
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public void Edit(TEntity entity)
        {
            var editedEntity = _context.Set<TEntity>().Where(e => e.Id == entity.Id).FirstOrDefault();
            editedEntity = entity;
            _context.Set<TEntity>().Update(editedEntity);
            _context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> Filter()
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
        public void SaveChanges() => _context.SaveChanges();
    }
}