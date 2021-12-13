using System;
using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using Core.DTO.Paging;
using Core.DTO.Product;
using Core.Extensions;
using DataLayer.ApplicationContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Implementations
{
    public class ProductService : IProductService
    {
        private EshopContext context;

        public ProductService(EshopContext context)
        {
            this.context = context;
        }


        public FilterProductsDTO GetProductsByFilter(FilterProductsDTO filter)
        {
            var query = context.Products.AsQueryable().SetProductFilters(filter);

            var count = (int)Math.Ceiling(query.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = query.OrderByDescending(u => u.ProductId)
                .Pagging(pager)
                .AsNoTracking().ToList();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public Product GetProduct(int productId)
        {
            return context.Products.SingleOrDefault(s => s.ProductId == productId);
        }

        public List<Product> GetLatestProducts()
        {
            return context.Products.OrderByDescending(s => s.ProductId).Take(16).ToList();
        }

        public List<Product> GetMostSellProducts()
        {
            return context.Products
                .Include(s => s.OrderDetails)
                .ThenInclude(s => s.Order)
                .Where(s => s.OrderDetails.All(f => f.Order.IsFinaly))
                .OrderByDescending(s => s.OrderDetails.Sum(f => f.Count))
                .Take(8).ToList();
        }

        public List<Product> GetSuggestedProducts()
        {
            return context.Products.OrderBy(s => Guid.NewGuid()).Take(4).ToList();
        }

        public bool IsExistProductById(int productId)
        {
            return context.Products.Any(s => s.ProductId == productId);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
