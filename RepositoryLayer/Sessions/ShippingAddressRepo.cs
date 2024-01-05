using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Sessions
{
    public class ShippingAddressRepo : IShippingAddressRepo
    {
        private readonly IConfiguration _config;

        public ShippingAddressRepo(IConfiguration config)
        {
            _config = config;
        }

        public bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel)
        {
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                if (shipmentAddressModel.AddressType > 0 && shipmentAddressModel.AddressType < 4)
                {
                    SqlCommand cmd = new SqlCommand("spAddShippingAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ShippingAddress", shipmentAddressModel.ShippingAddress);
                    cmd.Parameters.AddWithValue("@ShippingCity", shipmentAddressModel.ShippingCity);
                    cmd.Parameters.AddWithValue("@ShippingState", shipmentAddressModel.ShippingState);
                    cmd.Parameters.AddWithValue("@AddressType", shipmentAddressModel.AddressType);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
