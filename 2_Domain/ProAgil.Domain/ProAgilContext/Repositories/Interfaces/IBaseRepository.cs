using ProAgil.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProAgil.Domain.ProAgilContext.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        bool Delete(TEntity entity);
        void Delete(int id);
        void Edit(TEntity entity);

        //read side (could be in separate Readonly Generic Repository)
        TEntity GetById(int id);
        
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

        //separate method SaveChanges can be helpful when using this pattern with UnitOfWork
        void SaveChanges();
    }
}