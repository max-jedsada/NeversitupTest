using DAL.Interfaces;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private IHttpContextAccessor _IHttpContextAccessor;

        private Dictionary<Type, object> _repositories;

        public TContext Context { get; set; }

        public UnitOfWork(TContext context, IHttpContextAccessor IHttpContextAccessor)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public IRepository<TEntity> Repo<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context, _IHttpContextAccessor);

            return (IRepository<TEntity>)_repositories[type];
        }

        public IDbContextTransaction BeginTransaction()
        {
            return this.Context.Database.BeginTransaction();
        }
    }
}
