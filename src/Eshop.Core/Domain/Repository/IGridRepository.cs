using Eshop.Core.Domain.Bases;
using Eshop.Core.Domain.Interfaces;
using Eshop.Core.Domain.Specification;

namespace Eshop.Core.Domain.Repository;

public interface IGridRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
{
    ValueTask<long> CountAsync(IGridSpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(IGridSpecification<TEntity> spec);
}