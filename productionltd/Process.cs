using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    class Process
    {
        public int ID;
        public int Duration { get; set; }
        public Product Product { get; set; }
        public Machine Machine { get; set; }

        public Process(int duration, Product product, Machine machine)
        {
            Duration = duration;
            Product = product;
            Machine = machine;
        }
        public void Save()
        {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addProcess", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@duration", Duration));
                cmd.Parameters.Add(new SqlParameter("@product", Product.ID));
                cmd.Parameters.Add(new SqlParameter("@machine", Machine.ID));

                ID = (int)cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                new Alert(e.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
