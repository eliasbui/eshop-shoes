using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Eshop.Infrastructure.EfCore;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}