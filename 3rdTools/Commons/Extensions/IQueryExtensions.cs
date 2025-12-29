using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions;

public static class IQueryExtensions
{
    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }

    public static IQueryable<T> OrderByIf<T>(this IQueryable<T> query, bool condition, string property)
    {
        return condition
            ? query.OrderBy(property)
            : query;
    }
    public static IOrderedQueryable<T> OrderBy<T>(
    this IQueryable<T> source,
    string property)
    {
        bool isFirstProperty = true;
        string[] props = property.Split(',');
        foreach (string prop in props)
        {
            var filed = prop.Trim().Split(' ')[0];
            var direction = prop.Trim().Split(' ')[1];
            if (direction.ToUpper() == "ASC")
            {
                if (isFirstProperty)
                {
                }
                source = isFirstProperty ? source.OrderByAscending(filed) : ((IOrderedQueryable<T>)source).ThenByAscending(filed);
            }
            else if (direction.ToUpper() == "DESC")
            {
                source = isFirstProperty ? source.OrderByDescending(filed) : ((IOrderedQueryable<T>)source).ThenByDescending(filed);
            }
            else
            {
                throw new NotSupportedException("Wrong Direction");
            }
            isFirstProperty = false;
        }
        return (IOrderedQueryable<T>)source;
    }
    public static IOrderedQueryable<T> OrderByAscending<T>(
    this IQueryable<T> source,
    string property)
    {
        return ApplyOrder<T>(source, property, "OrderBy");
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(
        this IQueryable<T> source,
        string property)
    {
        return ApplyOrder<T>(source, property, "OrderByDescending");
    }

    public static IOrderedQueryable<T> ThenByAscending<T>(
        this IOrderedQueryable<T> source,
        string property)
    {
        return ApplyOrder<T>(source, property, "ThenBy");
    }

    public static IOrderedQueryable<T> ThenByDescending<T>(
        this IOrderedQueryable<T> source,
        string property)
    {
        return ApplyOrder<T>(source, property, "ThenByDescending");
    }

    private static IOrderedQueryable<T> ApplyOrder<T>(
        IQueryable<T> source,
        string property,
        string methodName)
    {
        string[] props = property.Split('.');
        Type type = typeof(T);
        ParameterExpression arg = Expression.Parameter(type, "x");
        Expression expr = arg;
        foreach (string prop in props)
        {
            // use reflection (not ComponentModel) to mirror LINQ
            PropertyInfo pi = type.GetProperty(prop);
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;
        }
        Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
        LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

        object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
        return (IOrderedQueryable<T>)result;
    }
}
