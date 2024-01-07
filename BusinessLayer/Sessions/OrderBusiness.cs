using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepo orderRepo;

        public OrderBusiness(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        public bool AddOrder(int UserId, OrderModel orderModel)
        {
            return orderRepo.AddOrder(UserId, orderModel);
        }

        public List<OrderModel> GetOrderList(int UserId)
        {
            return orderRepo.GetOrderList(UserId);
        }

        public bool DeleteOrder(int UserId, int OrderId)
        {
            return orderRepo.DeleteOrder(UserId, OrderId);
        }
    }
}
