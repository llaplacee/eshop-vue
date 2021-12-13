using System.Linq;
using Core.DTO.Paging;

namespace Core.Extensions
{
    public static class PagingExtension
    {
        public static IQueryable<T> Pagging<T>(this IQueryable<T> queryable, BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
