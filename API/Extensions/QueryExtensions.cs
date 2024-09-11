using API.Models;
using System.Linq.Expressions;

namespace API.Extensions;

public static class QueryExtensions
{
    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity, TKey>(
        this IQueryable<TEntity> queryable,
        SortDirection sortDirection,
        Expression<Func<TEntity, TKey>> keySelector
    ) => sortDirection switch
    {
        SortDirection.Ascending => queryable.OrderBy(keySelector),
        SortDirection.Descending => queryable.OrderByDescending(keySelector)
    };
}

//public static class Extensions
//{
//    public static bool ContainsAny<T>(this IQueryable<T> haystacks, IQueryable<T> needles)
//    {
//        foreach (var needle in needles)
//        {
//            if (haystacks.Contains(needle))
//                return true;
//        }

//        return false;
//    }
//}