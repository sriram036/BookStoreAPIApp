using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRepo
    {
        bool AddToWishList(int UserId, int Id);
        List<int> GetWishList(int Id);
        bool DeleteWishList(int WishId, int UserId);
    }
}