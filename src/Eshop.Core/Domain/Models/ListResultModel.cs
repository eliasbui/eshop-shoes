using System.Collections.Generic;

namespace Eshop.Core.Domain.Models;

public record ListResultModel<T>(List<T> Items, long TotalItems, int Page, int PageSize) where T : notnull
{
    public static ListResultModel<T> Create(List<T> items, long totalItems = 0, int page = 1, int pageSize = 20)
    {
        return new ListResultModel<T>(items, totalItems, page, pageSize);
    }
}