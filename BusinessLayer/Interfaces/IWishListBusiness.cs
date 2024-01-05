using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBusiness
    {
        bool AddToWishList(int UserId, int Id);
        List<int> GetWishList(int Id);
        bool DeleteWishList(int WishId, int UserId);
    }
}