using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class CartBusiness : ICartBusiness
    {
        private readonly ICartRepo cartRepo;

        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }

        public bool AddToCart(int UserId, CartModel cartModel)
        {
            return cartRepo.AddToCart(UserId, cartModel);
        }

        public List<CartModel> GetCarts(int id)
        {
            return cartRepo.GetCarts(id);
        }

        public bool UpdateCart(int count, int cartId, int UserId)
        {
            return cartRepo.UpdateCart(count, cartId, UserId);
        }

        public bool DeleteCart(int cartId, int UserId)
        {
            return cartRepo.DeleteCart(cartId, UserId);
        }
    }
}
