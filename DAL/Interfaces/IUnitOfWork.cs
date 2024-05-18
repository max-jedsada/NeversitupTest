using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repo<TEntity>() where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync();

        IDbContextTransaction BeginTransaction();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
                    where TContext : DbContext
    {
        TContext Context { get; }
    }
}
