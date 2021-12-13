using System;
using System.Collections.Generic;
using Core.DTO.Product;
using DataLayer.Entities;

namespace Core.Contracts
{
    public interface IProductService : IDisposable
    {
        FilterProductsDTO GetProductsByFilter(FilterProductsDTO filter);
        Product GetProduct(int productId);
        List<Product> GetLatestProducts();
        List<Product> GetMostSellProducts();
        List<Product> GetSuggestedProducts();
        bool IsExistProductById(int productId);
    }
}