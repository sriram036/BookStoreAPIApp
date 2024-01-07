using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;
using System.Xml.Linq;

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

        public List<ShipmentAddressModel> GetShippingAddress(int UserId)
        {
            List<ShipmentAddressModel> shippingAddresses = new List<ShipmentAddressModel>();

            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetShippingAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    ShipmentAddressModel shipmentAddress = new ShipmentAddressModel();
                    shipmentAddress.ShippingAddress = Reader["ShippingAddress"].ToString();
                    shipmentAddress.ShippingCity = Reader["ShippingCity"].ToString();
                    shipmentAddress.ShippingState = Reader["ShippingState"].ToString();
                    shipmentAddress.AddressType = Convert.ToInt32(Reader["AddressType"]);

                    shippingAddresses.Add(shipmentAddress);
                }
            }
            return shippingAddresses;
        }

        public bool DeleteShippingAddress(int UserId, int ShippingId)
        {
            List<int> shippingIds = new List<int>();
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetShippingAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    int Ids = Convert.ToInt32(Reader["ShippingAddressId"]);

                    shippingIds.Add(Ids);
                }
                if (ShippingId == shippingIds.Find(id => id.Equals(ShippingId)))
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteShippingAddress", con);
                    cmdDelete.CommandType = CommandType.StoredProcedure;
                    cmdDelete.Parameters.AddWithValue("@ShippingAddressId", ShippingId);
                    cmdDelete.ExecuteNonQuery();
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
