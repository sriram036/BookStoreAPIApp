using ModelLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBusiness
    {
        bool AddOrder(int UserId, OrderModel orderModel);

        List<OrderModel> GetOrderList(int UserId);

        bool DeleteOrder(int UserId, int OrderId);
    }
}