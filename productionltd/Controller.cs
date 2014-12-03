using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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

            List<Product> products = new List<Product>();

            try {
                conn.Open();

                SqlCommand cmd = new SqlCommand("getProducts", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@productType";
                parameter.Value = productType ? 0 : 1;
                cmd.Parameters.Add(parameter);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read()) {
                    products.Add(new Product(reader["Name"].ToString(), Convert.ToBoolean(reader["ProductType"]), reader["Size"].ToString()) { ID = int.Parse(reader["ID"].ToString()) });
                }
                reader.Close();
            }
            catch (SqlException e) {
                MessageBox.Show(e.Message);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
            
            return products;
        }
        public List<Machine> getMachines() {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);
            
            List<Machine> machines = new List<Machine>();

            try {
                conn.Open();

                SqlCommand cmd = new SqlCommand("getMachines", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    machines.Add(new Machine(int.Parse(reader["id"].ToString()), reader["Name"].ToString(), int.Parse(reader["Quantity"].ToString())));
                }

                reader.Close();
            }
            catch (SqlException e) {
                MessageBox.Show(e.Message);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
            
            return machines;
        }
        public List<MachineBooking> getMachineBookings(int machineID) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getMachineBookings", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Machine_FK";
            parameter.Value = machineID;
            cmd.Parameters.Add(parameter);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MachineBooking> machineBookings = new List<MachineBooking>();

            while (reader.Read()) {
                machineBookings.Add(new MachineBooking(int.Parse(reader["id"].ToString()), Convert.ToDateTime(reader["StartTime"]), Convert.ToDateTime(reader["EndTime"])));
            }
            reader.Close();
            conn.Close();
            conn.Dispose();
            return machineBookings;
        }

        internal void NewOrder(string name, string company, Dictionary<Product, int> products) {
            throw new NotImplementedException();
        }
    }
}
