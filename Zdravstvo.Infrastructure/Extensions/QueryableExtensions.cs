using System.Linq;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagingParams p)
        {
            var page = p.Page < 1 ? 1 : p.Page;
            var pageSize = p.PageSize < 1 ? 10 : (p.PageSize > p.MaxPageSize ? p.MaxPageSize : p.PageSize);
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
