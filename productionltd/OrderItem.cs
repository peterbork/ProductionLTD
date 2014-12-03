using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    class OrderItem
    {
        private int ID;
        public int Amount { get; set; }
        public Product Product { get; set; }
        public List<MachineBooking> Bookings { get; set; }

        public OrderItem(int amount, Product product) {
            Amount = amount;
            Product = product;
        }
        public void Save(int order) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addOrderItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@amount", Amount));
                cmd.Parameters.Add(new SqlParameter("@order", order));
                cmd.Parameters.Add(new SqlParameter("@product", Product.ID));

                cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
