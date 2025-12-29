using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class IQueryExtensions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(items, count, pageNumber, pageSize);

        }
    }
}
