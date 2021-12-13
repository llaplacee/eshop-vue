using System;
using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using DataLayer.ApplicationContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Implementations
{
    public class OrderService : IOrderService
    {
        private EshopContext _context;

        public OrderService(EshopContext context)
        {
            _context = context;
        }

        public void AddProductToUserOrder(int userId, int productId, int? count)
        {
            var product = _context.Products.SingleOrDefault(s => s.ProductId == productId);

            if (product != null)
            {
                Order openOrder;

                if (!IsExistUserOpenOrder(userId)) AddOrder(userId);

                openOrder = GetUserOpenOrder(userId);

                var newDetail = new OrderDetail
                {
                    OrderId = openOrder.OrderId,
                    ProductId = productId,
                    Count = (count != null && count >= 0) ? count.Value : 1,
                    Price = product.Price
                };

                if (IsExistDetailForOrder(newDetail))
                {
                    var detail = GetOrderDetail(productId, openOrder.OrderId);
                    detail.Count += newDetail.Count;
                    _context.SaveChanges();
                }
                else
                {
                    _context.OrderDetails.Add(newDetail);
                    _context.SaveChanges();
                }
            }
        }


        public bool IsExistUserOpenOrder(int userId)
        {
            return _context.Orders.Any(s => s.UserId == userId && !s.IsFinaly);
        }

        public Order GetUserOpenOrder(int userId)
        {
            return _context.Orders.Include(s => s.OrderDetails).ThenInclude(s => s.Product).SingleOrDefault(s => s.UserId == userId && !s.IsFinaly);
        }

        public void AddOrder(int userId)
        {
            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.Now,
            };

            _context.Add(order);
            _context.SaveChanges();
        }

        public bool IsExistDetailForOrder(OrderDetail detail)
        {
            return _context.OrderDetails.Any(s => s.OrderId == detail.OrderId && s.ProductId == detail.ProductId);
        }

        public OrderDetail GetOrderDetail(int productId, int orderId)
        {
            return _context.OrderDetails.SingleOrDefault(s => s.OrderId == orderId && s.ProductId == productId);
        }

        public List<OrderDetail> GetUserShopCart(int userId)
        {
            return _context.OrderDetails.Include(s => s.Order).Where(s => !s.Order.IsFinaly && s.Order.UserId == userId).ToList();
        }

        public bool IsExistOrderDetailById(int detailId)
        {
            return _context.OrderDetails.Any(s => s.OrderDetailId == detailId);
        }

        public void DeleteOrderDetail(int detailId)
        {
            var detail = _context.OrderDetails.SingleOrDefault(s => s.OrderDetailId == detailId);

            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                _context.SaveChanges();
            }
        }

        public bool IsDetailForUser(int detailId, int userId)
        {
            var detail = _context.OrderDetails.Include(s => s.Order).SingleOrDefault(s => s.OrderDetailId == detailId);

            if (detail != null && detail.Order.UserId == userId) return true;

            return false;
        }

        public void EditOrder(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
