using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApplicationDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> List()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Remove(Guid id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
