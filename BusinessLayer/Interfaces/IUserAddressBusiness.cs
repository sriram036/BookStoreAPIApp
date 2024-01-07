using ModelLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IUserAddressBusiness
    {
        UserAddressModel AddUserAddress(int UserId, UserAddressModel userAddressModel);

        List<UserAddressModel> GetUserAddresses(int UserId);

        bool UpdateUserAddress(int UserId, UserAddressModel userAddressModel);

        bool DeleteUser(int UserId, int Type);
    }
}