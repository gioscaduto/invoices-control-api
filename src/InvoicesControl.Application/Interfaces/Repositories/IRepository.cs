using InvoicesControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> List();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
