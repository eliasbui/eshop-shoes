using Eshop.Core.Domain.Bases;
using Eshop.Core.Domain.Interfaces;
using Eshop.Core.Domain.Specification;

namespace Eshop.Core.Domain.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
{
    TEntity FindById(Guid id);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}