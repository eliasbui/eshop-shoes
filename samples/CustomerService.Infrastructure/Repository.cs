using CustomerService.Infrastructure.Data;
using Eshop.Core.Domain.Bases;
using Eshop.Infrastructure.EfCore;

namespace CustomerService.Infrastructure;

public class Repository<TEntity>(MainDbContext dbContext) : RepositoryBase<MainDbContext, TEntity>(dbContext)
    where TEntity : EntityRootBase;