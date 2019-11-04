using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sop.Data.Inrefaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> where(Expression<Func<TEntity, bool>> where = null);
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object Id);
    }
}