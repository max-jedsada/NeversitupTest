using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> get();

        IEnumerable<T> get(Expression<Func<T, bool>> predicate);

        IQueryable<T> getQuery(Expression<Func<T, bool>> predicate);

        IQueryable<T> include(params Expression<Func<T, object>>[] expressions);

        IQueryable<T> include(params string[] includedProperties);

        T single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        void add(T entity);

        void add(params T[] entities);

        void add(IEnumerable<T> entities);

        void update(T entity);

        void update(params T[] entities);

        void update(IEnumerable<T> entities);

        void delete(T entity);

        void softDelete(T entity);

        void softDelete(params T[] entities);

        void softDelete(IEnumerable<T> entities);

        void setValues(T existingEntity, T updatedEntity);

        T decorateInsertEntity(T entity);

        T[] decorateInsertEntity(T[] entities);

        IEnumerable<T> decorateInsertEntity(IEnumerable<T> entities);

        T decorateUpdateEntity(T entity);

        DbSet<T> getDbSet();
    }
}