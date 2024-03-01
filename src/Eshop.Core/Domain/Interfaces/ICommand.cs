using Eshop.Core.Domain.Models;
using MediatR;

namespace Eshop.Core.Domain.Interfaces;

public interface ICommand<T> : IRequest<ResultModel<T>>
    where T : notnull
{
}