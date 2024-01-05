using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class WishListBusiness : IWishListBusiness
    {
        private readonly IWishListRepo wishListRepo;

        public WishListBusiness(IWishListRepo wishListRepo)
        {
            this.wishListRepo = wishListRepo;
        }

        public bool AddToWishList(int UserId, int Id)
        {
            return wishListRepo.AddToWishList(UserId, Id);
        }

        public List<int> GetWishList(int Id)
        {
            return wishListRepo.GetWishList(Id);
        }

        public bool DeleteWishList(int WishId, int UserId)
        {
            return wishListRepo.DeleteWishList(WishId, UserId);
        }
    }
}
