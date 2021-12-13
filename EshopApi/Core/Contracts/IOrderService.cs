using System;
using System.Collections.Generic;
using DataLayer.Entities;

namespace Core.Contracts
{
    public interface IOrderService : IDisposable
    {
        void AddProductToUserOrder(int userId, int productId, int? count);
        bool IsExistUserOpenOrder(int userId);
        Order GetUserOpenOrder(int userId);
        void AddOrder(int userId);
        bool IsExistDetailForOrder(OrderDetail detail);
        OrderDetail GetOrderDetail(int productId, int orderId);
        List<OrderDetail> GetUserShopCart(int userId);
        bool IsExistOrderDetailById(int detailId);
        void DeleteOrderDetail(int detailId);
        bool IsDetailForUser(int detailId, int userId);
        void EditOrder(Order order);
    }
}