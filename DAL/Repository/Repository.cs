using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IHttpContextAccessor _IHttpContextAccessor;

        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public string userId
        {
            get
            {
                return _IHttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            }
        }

        public string currentUserId { get; set; }

        public Repository(DbContext context, IHttpContextAccessor IHttpContextAccessor)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        public T decorateInsertEntity(T entity)
        {
            if (typeof(T).GetProperty("CreatedDate") != null)
            {
                entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now);
            }

            if (typeof(T).GetProperty("ModifiedDate") != null)
            {
                entity.GetType().GetProperty("ModifiedDate").SetValue(entity, DateTime.Now);
            }

            if (typeof(T).GetProperty("IsDelete") != null)
            {
                entity.GetType().GetProperty("IsDelete").SetValue(entity, false);
            }

            if (string.IsNullOrEmpty(userId))
            {

            }

            return entity;
        }

        public T[] decorateInsertEntity(T[] entities)
        {
            return entities.Select(decorateInsertEntity).ToArray();
        }

        public IEnumerable<T> decorateInsertEntity(IEnumerable<T> entities)
        {
            return entities.Select(decorateInsertEntity).AsEnumerable();
        }

        public T decorateUpdateEntity(T entity)
        {
            if (typeof(T).GetProperty("ModifiedDate") != null)
            {
                entity.GetType().GetProperty("ModifiedDate").SetValue(entity, DateTime.Now);
            }

            return entity;
        }

        public T decorateSoftDeleteEntity(T entity)
        {
            if (typeof(T).GetProperty("IsDelete") != null)
            {
                entity.GetType().GetProperty("IsDelete").SetValue(entity, true);
            }
            if (typeof(T).GetProperty("DeletedDate") != null)
            {
                entity.GetType().GetProperty("DeletedDate").SetValue(entity, DateTime.Now);
            }

            return entity;
        }

        public T decorateSetValuesEntity(T existingEntity, T updatedEntity)
        {
            if (typeof(T).GetProperty("IsDelete") != null)
            {
                var existingValue = existingEntity.GetType().GetProperty("isDelete").GetValue(existingEntity);
                updatedEntity.GetType().GetProperty("IsDelete").SetValue(updatedEntity, existingValue);
            }
            if (typeof(T).GetProperty("CreatedDate") != null)
            {
                var existingValue = existingEntity.GetType().GetProperty("CreatedDate").GetValue(existingEntity);
                updatedEntity.GetType().GetProperty("CreatedDate").SetValue(updatedEntity, existingValue);
            }
            if (typeof(T).GetProperty("CreatedBy") != null)
            {
                var existingValue = existingEntity.GetType().GetProperty("CreatedBy").GetValue(existingEntity);
                updatedEntity.GetType().GetProperty("CreatedBy").SetValue(updatedEntity, existingValue);
            }

            var today = DateTime.Now;
            if (typeof(T).GetProperty("ModifiedDate") != null)
            {
                updatedEntity.GetType().GetProperty("ModifiedDate").SetValue(updatedEntity, today);
            }

            return updatedEntity;
        }

        public void add(T entity)
        {
            entity = decorateInsertEntity(entity);

            _dbSet.Add(entity);
        }

        public void add(params T[] entities)
        {
            var tempEntities = entities.AsEnumerable<T>()
                                       .Select(entity =>
                                       {
                                           entity = decorateInsertEntity(entity);
                                           return entity;
                                       });

            _dbSet.AddRange(tempEntities);
        }

        public void add(IEnumerable<T> entities)
        {
            var tempEntities = entities.Select(entity =>
            {
                entity = decorateInsertEntity(entity);
                return entity;
            });

            _dbSet.AddRange(tempEntities);
        }

        public T single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();

            return query.FirstOrDefault();
        }

        public DbSet<T> getDbSet()
        {
            return _dbSet;
        }

        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<T> get()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IEnumerable<T> res = _dbSet.Where(predicate).AsEnumerable();
                return res;
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                return null;
            }
        }

        public IQueryable<T> getQuery(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void update(T entity)
        {
            entity = decorateUpdateEntity(entity);
            _dbSet.Update(entity);
        }

        public void update(params T[] entities)
        {
            var tempEntities = entities.AsEnumerable<T>()
                                       .Select(entity =>
                                       {
                                           entity = decorateUpdateEntity(entity);
                                           return entity;
                                       });

            _dbSet.UpdateRange(tempEntities);
        }

        public void update(IEnumerable<T> entities)
        {
            var tempEntities = entities.Select(entity =>
            {
                entity = decorateUpdateEntity(entity);
                return entity;
            });

            _dbSet.UpdateRange(tempEntities);
        }

        public void delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void softDelete(T entity)
        {
            entity = decorateSoftDeleteEntity(entity);
            _dbSet.Update(entity);
        }

        public void softDelete(params T[] entities)
        {
            var tempEntities = entities.AsEnumerable<T>()
                                       .Select(entity =>
                                       {
                                           entity = decorateSoftDeleteEntity(entity);
                                           return entity;
                                       });

            _dbSet.UpdateRange(tempEntities);
        }

        public void softDelete(IEnumerable<T> entities)
        {
            var tempEntities = entities.Select(entity =>
            {
                entity = decorateSoftDeleteEntity(entity);
                return entity;
            });

            _dbSet.UpdateRange(tempEntities);
        }

        public IQueryable<T> include(params Expression<Func<T, object>>[] expressions)
        {
            var entities = _dbSet.AsQueryable();
            foreach (var includedPropery in expressions)
            {
                entities = entities.Include(includedPropery);
            }

            return entities;
        }

        public IQueryable<T> include(params string[] includedProperties)
        {
            var entities = _dbSet.AsQueryable();
            foreach (var property in includedProperties)
            {
                entities = entities.Include(property);
            }

            return entities;
        }

        public void setValues(T existingEntity, T updatedEntity)
        {
            decorateSetValuesEntity(existingEntity, updatedEntity);
            _dbContext.Entry<T>(existingEntity).CurrentValues.SetValues(updatedEntity);
        }
    }
}