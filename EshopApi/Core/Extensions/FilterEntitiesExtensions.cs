using System.Linq;
using Core.DTO.Product;
using Core.DTO.User;
using DataLayer.Entities;

namespace Core.Extensions
{
    public static class FilterEntitiesExtensions
    {
        #region User

        public static IQueryable<User> SetUserFilters(this IQueryable<User> queryable, GetUsersByFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                queryable = queryable.Where(s => s.Name.Contains(filter.UserName));
            }

            return queryable;
        }

        #endregion

        #region Product

        public static IQueryable<Product> SetProductFilters(this IQueryable<Product> queryable, FilterProductsDTO filter)
        {
            if (!string.IsNullOrEmpty(filter.Title))
            {
                queryable = queryable.Where(s => s.ProductName.Contains(filter.Title) || s.Description.Contains(filter.Title));
            }

            return queryable;
        }

        #endregion
    }
}
