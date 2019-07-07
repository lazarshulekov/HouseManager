using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        void Delete(object id);

        void Delete(TEntity entityToDelete);

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
    }
}