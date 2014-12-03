using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    class Order
    {
        public int ID;
        public string Name { get; set; }
        public string Company { get; set; }
        public DateTime Deadline { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order(string name, string company)
        {
            Name = name;
            Company = company;
        }
        public void Save(){
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@name", Name));
                cmd.Parameters.Add(new SqlParameter("@Company", Company));

                cmd.ExecuteNonQuery();
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                conn.Close();
                conn.Dispose();
            }

            foreach (OrderItem item in OrderItems) {
                item.Save(ID);
            }

            new Alert("Order added!");
           
        }

    }
}
