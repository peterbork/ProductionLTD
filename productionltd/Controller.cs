﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    class Controller
    {
        private List<Order> orders;

        public void NewOrder(string name, string company)
        {
            Order p = new Order(name, company);
        }
        public void NewOrderItem()
        {

        }
        public void AddToOrderList()
        {

        }
        public List<Product> getProducts(bool productType = false)
        {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getProducts", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@productType";
            parameter.Value = productType ? 0 : 1;
            cmd.Parameters.Add(parameter);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> products = new List<Product>();

            while (reader.Read()) {
                products.Add(new Product(reader["Name"].ToString(), Convert.ToBoolean(reader["ProductType"]), reader["Size"].ToString()));
            }
            reader.Close();
            conn.Close();
            conn.Dispose();
            return products;
        }
        public List<Machine> getMachines() {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getMachines", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Machine> machines = new List<Machine>();

            while (reader.Read()) {
                machines.Add(new Machine(int.Parse(reader["id"].ToString()), reader["Name"].ToString(), int.Parse(reader["Quantity"].ToString())));
            }
            reader.Close();
            conn.Close();
            conn.Dispose();
            return machines;
        }
    }
}
