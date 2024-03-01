using Eshop.Core.Domain.Models;
using MediatR;

namespace Eshop.Core.Domain.Interfaces;

public interface IQuery<T> : IRequest<ResultModel<T>>
    where T : notnull
{
}