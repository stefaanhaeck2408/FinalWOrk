using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DL.Helpers
{
    public static class IncludeExtension
    {        
            public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
            {
                if (includes == null)
                    return query;

                var includable = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                return includable;
            }
        }
    
}
