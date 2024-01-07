using ModelLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRepo
    {
        bool AddOrder(int UserId, OrderModel orderModel);

        List<OrderModel> GetOrderList(int UserId);

        bool DeleteOrder(int UserId, int OrderId);
    }
}