using System.Collections.Generic;
using Core.DTO.Paging;

namespace Core.DTO.Product
{
    public class FilterProductsDTO : BasePaging
    {
        public string Title { get; set; }

        public List<DataLayer.Entities.Product> Products { get; set; }

        public FilterProductsDTO SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;

            SkipEntity = paging.SkipEntity;

            TakeEntity = paging.TakeEntity;

            ActivePage = paging.PageId;

            EndPage = paging.EndPage;

            PageCount = paging.PageCount;

            StartPage = paging.StartPage;

            return this;
        }

        public FilterProductsDTO SetProducts(List<DataLayer.Entities.Product> products)
        {
            Products = products;

            return this;
        }
    }
}
