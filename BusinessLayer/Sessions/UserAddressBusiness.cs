using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class UserAddressBusiness : IUserAddressBusiness
    {
        private readonly IUserAddressRepo userAddressRepo;

        public UserAddressBusiness(IUserAddressRepo userAddressRepo)
        {
            this.userAddressRepo = userAddressRepo;
        }

        public UserAddressModel AddUserAddress(int UserId, UserAddressModel userAddressModel)
        {
            return userAddressRepo.AddUserAddress(UserId, userAddressModel);
        }

        public List<UserAddressModel> GetUserAddresses(int UserId)
        {
            return userAddressRepo.GetUserAddresses(UserId);
        }

        public bool UpdateUserAddress(int UserId, UserAddressModel userAddressModel)
        {
            return userAddressRepo.UpdateUserAddress(UserId, userAddressModel);
        }

        public bool DeleteUser(int UserId, int Type)
        {
            return (userAddressRepo.DeleteUser(UserId, Type));
        }
    }
}
