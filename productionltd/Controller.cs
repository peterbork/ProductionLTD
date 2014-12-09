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
        public List<Machine> machines;
        public static MainWindow mainwindow;

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
        public List<Process> getProcesses(Product product) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            product.Processes = new List<Process>();

            getMachines();

            try {
                conn.Open();

                SqlCommand cmd = new SqlCommand("getProcesses", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@product";
                parameter.Value = product.ID;
                cmd.Parameters.Add(parameter);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    Machine pmachine = new Machine();
                    foreach (Machine machine in machines) {
                        if (machine.ID == int.Parse(reader["Machine_FK"].ToString()))
                            pmachine = machine;
                    }
                    product.Processes.Add(new Process(int.Parse(reader["Duration"].ToString()), product, pmachine) { ID = int.Parse(reader["ID"].ToString()) });
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

            return product.Processes;
        }
        public List<Machine> getMachines() {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);
            
            machines = new List<Machine>();

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
        public List<MachineBooking> getMachineBookings(Machine machine) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getMachineBookings", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@machine";
            parameter.Value = machine.ID;
            cmd.Parameters.Add(parameter);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MachineBooking> machineBookings = new List<MachineBooking>();

            while (reader.Read()) {
                machineBookings.Add(new MachineBooking(Convert.ToDateTime(reader["StartTime"]), Convert.ToDateTime(reader["EndTime"]), machine));
            }
            reader.Close();
            conn.Close();
            conn.Dispose();
            return machineBookings;
        }
        public List<MachineBooking> getMachineBookings(DateTime date)
        {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getMachineBookings", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@date";
            parameter.Value = date;
            cmd.Parameters.Add(parameter);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MachineBooking> machineBookings = new List<MachineBooking>();
            List<Machine> machines = getMachines();
            while (reader.Read())
            {
                foreach (Machine m in machines)
	            {
		            if (m.ID == (int)reader["Machine_FK"])
	                {
		                 machineBookings.Add(new MachineBooking((DateTime)reader["StartTime"], (DateTime)reader["EndTime"], m));
	                }
	            }
            }
            reader.Close();
            conn.Close();
            conn.Dispose();
            return machineBookings;
        }

        /*public List<MachineBooking> getMachineBookings(DateTime afterTime) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getMachineBookings", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@aftertime";
            parameter.Value = afterTime;
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
        }*/

        internal void NewOrder(string name, string company, Dictionary<Product, int> products) {
            Order o = new Order(name, company);
            foreach (KeyValuePair<Product, int> item in products) {
                o.OrderItems.Add(new OrderItem(item.Value, item.Key));
            }
            o.Save();
        }
        public Product addSpecialProduct(string name, string size, List<Process> processList)
        {
            Product p = new Product(name, false, size);
            p.Save();
            foreach (Process process in processList)
	        {
                process.Product = p;
                p.Processes.Add(process);
                if (process.Duration > 0)
                {
                    process.Save();
                }
	        }
            return p;
        }
    }
}
