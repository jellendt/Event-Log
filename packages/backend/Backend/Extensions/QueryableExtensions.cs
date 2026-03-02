
using Backend.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            int totalCount = await queryable.CountAsync();
            List<T> items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<T>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
