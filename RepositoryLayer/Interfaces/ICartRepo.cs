using ModelLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRepo
    {
        bool AddToCart(int UserId, CartModel cartModel);
        List<CartModel> GetCarts(int id);
        bool UpdateCart(int count, int cartId, int UserId);
        bool DeleteCart(int cartId, int UserId);
    }
}